using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace EstimationPortal.Utility
{
    public class FileUploadUtility
    {
        public static string FileUpload(HttpPostedFileBase file, string fileName, string folderPath, string serverPathToSave)
        {
            if (file.ContentLength > 0)
            {
                string directory = Path.Combine(serverPathToSave);

                var dirInfo = Directory.CreateDirectory(directory);

                if (file.ContentLength > 0)
                {
                    fileName += Path.GetExtension(file.FileName);
                    var path = serverPathToSave + fileName;
                    if (File.Exists(path))
                        File.Delete(path);
                    file.SaveAs(path);
                    return folderPath + fileName;
                }
            }
            return "";
        }
    }
}