using System;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text; 
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using SUP_CORE.EFModel;
using SUP_BAL.IRepository;
using SUP_DAL.EFContextProvider;
using System.Linq;

namespace NJoinee.Controllers
{
    public class ChatController : Controller
    {
        private readonly string openAiApiKey = "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";
        // GET: Chat


        private readonly IGenericRepository<ProjectMasters> _genericProjectMasterRepository;
        private readonly IGenericRepository<DocMasters> _genericDocMastersRepository;
        private readonly IGenericRepository<RevisionMasters> _genericRevMastersRepository;
        public ChatController( IGenericRepository<ProjectMasters> genericProjectMasterRepository,  IGenericRepository<DocMasters> genericDocMastersRepository, IGenericRepository<RevisionMasters> genericRevMastersRepository)
        {
            _genericProjectMasterRepository = genericProjectMasterRepository;
            _genericDocMastersRepository = genericDocMastersRepository;
            _genericRevMastersRepository = genericRevMastersRepository; 

        }

        [HttpGet]
        public ActionResult Chat()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> GetResponse(ChatRequest request)
        {
            // Step 1: Get Project using PO Number
            var project = _genericProjectMasterRepository.Find(p => p.PO == request.PONo ).FirstOrDefault();
            if (project == null)
            {
                return Json(new { response = "Project not found for PO No: " + request.PONo });
            }

            // Step 2: Use AI to interpret user query
            string aiAnalysis = await GetAiUnderstanding(request.Message);

            // Step 3: Search the database based on AI analysis
            string response = await ProcessUserQuery(aiAnalysis, project);

            return Json(new { response });
        }

        // OpenAI GPT Call
        private async Task<string> GetAiUnderstanding(string userInput)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {openAiApiKey}");

                var requestBody = new
                {
                    model = "gpt-4",
                    messages = new[]
                    {
                    new { role = "system", content = "You are an intelligent assistant that extracts document-related information." },
                    new { role = "user", content = userInput }
                }
                };

                var jsonRequest = JsonConvert.SerializeObject(requestBody);
                var httpContent = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

                var response = await client.PostAsync("https://api.openai.com/v1/chat/completions", httpContent);
                var jsonResponse = await response.Content.ReadAsStringAsync();
                dynamic parsedResponse = JsonConvert.DeserializeObject(jsonResponse);

                return parsedResponse.choices[0].message.content;
            }
        }

        // Process AI Analysis & Query Database
        private async Task<string> ProcessUserQuery(string aiQuery, ProjectMasters project)
        {
            // Check for pending documents
            if (aiQuery.Contains("pending") || aiQuery.Contains("not submitted"))
            {
                var pendingDocs = _genericDocMastersRepository.Find(d => d.ProjectMasterId == project.Id && d.IsFileUpload == false).ToList();
                return pendingDocs.Count > 0
                    ? $"There are {pendingDocs.Count} pending documents for project {project.PO}."
                    : "No pending documents found.";
            }

            // Check for specific document
            if (aiQuery.Contains("document") || aiQuery.Contains("file"))
            {
                string docName = aiQuery.Replace("document", "").Replace("file", "").Trim();
                var document = _genericDocMastersRepository.Find(d => d.ProjectMasterId == project.Id && d.DocName.Contains(docName)).FirstOrDefault();

                return document != null
                    ? $"Document found: {document.DocName}. Click here to access: <a href='/DocumentMaster/{document.FileName}' target='_blank'>Download</a>"
                    : "No matching document found.";
            }

            return "I couldn't understand your request. Please specify your query in detail.";
        }

    }

    public class ChatRequest
    {
        public string Message { get; set; }
        public string PONo { get; set; }  // <-- Use PO No instead of Project ID
    }

}