using Castle.Core.Logging;
using SUP_BAL.IRepository;
using SUP_CORE.EFModel;
using EstimationPortal.CustomeAttribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using EstimationPortal.CustomeAttribute;

namespace EstimationPortal.Controllers
{
    [SessionTimeout]
    [NoDirectAccess]
    public class LogsController : Controller
    {
        private readonly ILogger _logger;
        
        public LogsController(ILogger logger)
        {
            _logger = logger;
            
        }

        

        //[HttpGet]
        //public ActionResult SuppLogIndex()
        //{
        //    return View();
        //}
        //[HttpGet]
        //public ActionResult InvLogIndex()
        //{
        //    return View();
        //}
        //[HttpGet]
        //public ActionResult OpenPOLogIndex()
        //{
        //    return View();
        //}
        //[HttpGet]
        //public ActionResult MatLogIndex()
        //{
        //    return View();
        //}
        //[HttpPost]
        //public ActionResult GetSuppLgTableData()
        //{
        //    JsonResult result = new JsonResult();
        //    try
        //    {
        //        string search = Request.Form.GetValues("search[value]")[0];
        //        string draw = Request.Form.GetValues("draw")[0];
        //        string order = Request.Form.GetValues("order[0][column]")[0];
        //        string orderDir = Request.Form.GetValues("order[0][dir]")[0];
        //        int startRec = Convert.ToInt32(Request.Form.GetValues("start")[0]);
        //        int pageSize = Convert.ToInt32(Request.Form.GetValues("length")[0]);
        //        List<Supplier_Log> data = _genericSppLgRepository.GetAll().ToList();
        //        int totalRecords = data.Count;
        //        if (!string.IsNullOrEmpty(search) && !string.IsNullOrWhiteSpace(search))
        //        {
        //            data = data.Where(p => Convert.ToDateTime(p.UpdatedDateTime).ToString("yyyy-MM-dd").Contains(search)).ToList();
        //        }
        //        data = SortSpLTableData(order, orderDir, data);
        //        int recFilter = data.Count;
        //        data = data.Skip(startRec).Take(pageSize).ToList();
        //        var modifiedData = data.Select(d => new
        //        {
        //            d.Id,
        //            d.UpdatedDateTime,
        //            d.NumberOfRecords,
        //            d.Status
        //        });
        //        var result1 = Json(new
        //        {
        //            draw = Convert.ToInt32(draw),
        //            recordsTotal = totalRecords,
        //            recordsFiltered = recFilter,
        //            data = modifiedData,
        //            MaxJsonLength = int.MaxValue
        //        }, JsonRequestBehavior.AllowGet);
        //        result1.MaxJsonLength = Int32.MaxValue;
        //        return result1;
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.Write(ex);
        //    }
        //    return result;

        //}
         
        //private List<Supplier_Log> SortSpLTableData(string order, string orderDir, List<Supplier_Log> data)
        //{
        //    List<Supplier_Log> lst = new List<Supplier_Log>();
        //    try
        //    {
        //        switch (order)
        //        {
        //            case "0":
        //                lst = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.Id).ToList()
        //                                                                                         : data.OrderBy(p => p.Id).ToList();
        //                break;
        //            default:
        //                lst = orderDir.Equals("ASC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.UpdatedDateTime).ToList()
        //                                                                                         : data.OrderBy(p => p.UpdatedDateTime).ToList();
        //                break;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.Write(ex);
        //    }
        //    return lst;

        //}

        //[HttpPost]
        //public ActionResult GetInvLgTableData()
        //{
        //    JsonResult result = new JsonResult();
        //    try
        //    {
        //        string search = Request.Form.GetValues("search[value]")[0];
        //        string draw = Request.Form.GetValues("draw")[0];
        //        string order = Request.Form.GetValues("order[0][column]")[0];
        //        string orderDir = Request.Form.GetValues("order[0][dir]")[0];
        //        int startRec = Convert.ToInt32(Request.Form.GetValues("start")[0]);
        //        int pageSize = Convert.ToInt32(Request.Form.GetValues("length")[0]);
        //        List<Invoice_Log> data = _genericILgRepository.GetAll().ToList();
        //        int totalRecords = data.Count;
        //        if (!string.IsNullOrEmpty(search) && !string.IsNullOrWhiteSpace(search))
        //        {
        //            data = data.Where(p => Convert.ToDateTime(p.UpdatedDateTime).ToString("yyyy-MM-dd").Contains(search)).ToList();
        //        }
        //        data = SortInvLTableData(order, orderDir, data);
        //        int recFilter = data.Count;
        //        data = data.Skip(startRec).Take(pageSize).ToList();
        //        var modifiedData = data.Select(d => new
        //        {
        //            d.Id,
        //            d.UpdatedDateTime,
        //            d.NumberOfRecords,
        //            d.Status
        //        });
        //        var result1 = Json(new
        //        {
        //            draw = Convert.ToInt32(draw),
        //            recordsTotal = totalRecords,
        //            recordsFiltered = recFilter,
        //            data = modifiedData,
        //            MaxJsonLength = int.MaxValue
        //        }, JsonRequestBehavior.AllowGet);
        //        result1.MaxJsonLength = Int32.MaxValue;
        //        return result1;
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.Write(ex);
        //    }
        //    return result;

        //}
        //private List<Invoice_Log> SortInvLTableData(string order, string orderDir, List<Invoice_Log> data)
        //{
        //    List<Invoice_Log> lst = new List<Invoice_Log>();
        //    try
        //    {
        //        switch (order)
        //        {
        //            case "0":
        //                lst = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.Id).ToList()
        //                                                                                         : data.OrderBy(p => p.Id).ToList();
        //                break;
        //            default:
        //                lst = orderDir.Equals("ASC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.UpdatedDateTime).ToList()
        //                                                                                         : data.OrderBy(p => p.UpdatedDateTime).ToList();
        //                break;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.Write(ex);
        //    }
        //    return lst;

        //}

        //[HttpPost]
        //public ActionResult GetMtLgTableData()
        //{
        //    JsonResult result = new JsonResult();
        //    try
        //    {
        //        string search = Request.Form.GetValues("search[value]")[0];
        //        string draw = Request.Form.GetValues("draw")[0];
        //        string order = Request.Form.GetValues("order[0][column]")[0];
        //        string orderDir = Request.Form.GetValues("order[0][dir]")[0];
        //        int startRec = Convert.ToInt32(Request.Form.GetValues("start")[0]);
        //        int pageSize = Convert.ToInt32(Request.Form.GetValues("length")[0]);
        //        List<MaterialSubC_Log> data = _genericMSLgRepository.GetAll().ToList();
        //        int totalRecords = data.Count;
        //        if (!string.IsNullOrEmpty(search) && !string.IsNullOrWhiteSpace(search))
        //        {
        //            data = data.Where(p => Convert.ToDateTime(p.UpdatedDateTime).ToString("yyyy-MM-dd").Contains(search)).ToList();
        //        }
        //        data = SortMtTableData(order, orderDir, data);
        //        int recFilter = data.Count;
        //        data = data.Skip(startRec).Take(pageSize).ToList();
        //        var modifiedData = data.Select(d => new
        //        {
        //            d.Id,
        //            d.UpdatedDateTime,
        //            d.NumberOfRecords,
        //            d.Status
        //        });
        //        var result1 = Json(new
        //        {
        //            draw = Convert.ToInt32(draw),
        //            recordsTotal = totalRecords,
        //            recordsFiltered = recFilter,
        //            data = modifiedData,
        //            MaxJsonLength = int.MaxValue
        //        }, JsonRequestBehavior.AllowGet);
        //        result1.MaxJsonLength = Int32.MaxValue;
        //        return result1;
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.Write(ex);
        //    }
        //    return result;

        //}
        //private List<MaterialSubC_Log> SortMtTableData(string order, string orderDir, List<MaterialSubC_Log> data)
        //{
        //    List<MaterialSubC_Log> lst = new List<MaterialSubC_Log>();
        //    try
        //    {
        //        switch (order)
        //        {
        //            case "0":
        //                lst = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.Id).ToList()
        //                                                                                         : data.OrderBy(p => p.Id).ToList();
        //                break;
        //            default:
        //                lst = orderDir.Equals("ASC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.UpdatedDateTime).ToList()
        //                                                                : data.OrderBy(p => p.UpdatedDateTime).ToList();
        //                break;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.Write(ex);
        //    }
        //    return lst;

        //}

        //[HttpPost]
        //public ActionResult GetOpenPOLgTableData()
        //{
        //    JsonResult result = new JsonResult();
        //    try
        //    {
        //        string search = Request.Form.GetValues("search[value]")[0];
        //        string draw = Request.Form.GetValues("draw")[0];
        //        string order = Request.Form.GetValues("order[0][column]")[0];
        //        string orderDir = Request.Form.GetValues("order[0][dir]")[0];
        //        int startRec = Convert.ToInt32(Request.Form.GetValues("start")[0]);
        //        int pageSize = Convert.ToInt32(Request.Form.GetValues("length")[0]);
        //        List<OpenPO_Log> data = _genericPOLgRepository.GetAll().ToList();
        //        int totalRecords = data.Count;
        //        if (!string.IsNullOrEmpty(search) && !string.IsNullOrWhiteSpace(search))
        //        {
        //            data = data.Where(p => Convert.ToDateTime(p.UpdatedDateTime).ToString("yyyy-MM-dd").Contains(search)).ToList();
        //        }
        //        data = SortOpenPOTableData(order, orderDir, data);
        //        int recFilter = data.Count;
        //        data = data.Skip(startRec).Take(pageSize).ToList();
        //        var modifiedData = data.Select(d => new
        //        {
        //            d.Id,
        //            d.UpdatedDateTime,
        //            d.NumberOfRecords,
        //            d.Status
        //        });
        //        var result1 = Json(new
        //        {
        //            draw = Convert.ToInt32(draw),
        //            recordsTotal = totalRecords,
        //            recordsFiltered = recFilter,
        //            data = modifiedData,
        //            MaxJsonLength = int.MaxValue
        //        }, JsonRequestBehavior.AllowGet);
        //        result1.MaxJsonLength = Int32.MaxValue;
        //        return result1;
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.Write(ex);
        //    }
        //    return result;

        //}
        //private List<OpenPO_Log> SortOpenPOTableData(string order, string orderDir, List<OpenPO_Log> data)
        //{
        //    List<OpenPO_Log> lst = new List<OpenPO_Log>();
        //    try
        //    {
        //        switch (order)
        //        {
        //            case "0":
        //                lst = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.Id).ToList()
        //                                                                                         : data.OrderBy(p => p.Id).ToList();
        //                break;
        //            default:
        //                lst = orderDir.Equals("ASC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.UpdatedDateTime).ToList()
        //                                                                                         : data.OrderBy(p => p.UpdatedDateTime).ToList();
        //                break;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.Write(ex);
        //    }
        //    return lst;
        //}
    }
}