$(document).ready(function () {
    debugger
    $("#BremTag").hide();
    $("#SID").change(function () {
        debugger
        let Status = $("#SID option:selected").text();
        let SID = $("#SID option:selected").val();
        $("#Status").val(Status);
        if (SID == 3) { $("#BremTag").show(); }
        else { $("#BremTag").hide(); }
    });
    let CTID1 = $("#CountryMaster_Id option:selected").val();
    DropBindingStateList();
    if (parseInt(CTID1) == 2) {
        //india selected
        $("#MSMEdiv").show();
        //Enable
        $("#PAN").prop("disabled", false);
        $("#GST").prop("disabled", false);
        $("#PinCode").prop("disabled", false);
        $("#MobileNo").prop("disabled", false);
        $("#MobileNo2").prop("disabled", false);
        $("#StateMaster_Id").show();
        $("#StateName").hide();
    }
    else {

        $("#MSMEdiv").hide();
        $("#StateMaster_Id").hide();
        $("#StateName").show();
        $('#PAN').siblings('span.error').css('visibility', 'hidden');
        $('#PAN').siblings('span.valid').css('visibility', 'hidden');
        $('#GST').siblings('span.error').css('visibility', 'hidden');
        $('#GST').siblings('span.valid').css('visibility', 'hidden');
        $('#PinCode').siblings('span.error').css('visibility', 'hidden');
        $('#PinCode').siblings('span.valid').css('visibility', 'hidden');
        $('#MobileNo').siblings('span.error').css('visibility', 'hidden');
        $('#MobileNo').siblings('span.valid').css('visibility', 'hidden');
        $('#MobileNo2').siblings('span.error').css('visibility', 'hidden');
        $('#MobileNo2').siblings('span.valid').css('visibility', 'hidden');

        //Disable
        $("#PAN").prop("disabled", true);
        $("#GST").prop("disabled", true);
        $("#PinCode").prop("disabled", true);
        $("#MobileNo").prop("disabled", true);
        $("#MobileNo2").prop("disabled", true);

    }
    var currentDate = new Date();
    $('#ESTdate').datepicker({
        dateFormat: 'dd-mm-yy',
        maxDate: currentDate,
        changeYear: true,
        changeMonth: true
    }).attr('readonly', 'readonly');
    $("#CodeList").select2().show();
    $("#MsMECBMI1").click(function () {
        $("#MsMECBMI2").prop("checked", false);
    });
    $("#MsMECBMI2").click(function () {
        $("#MsMECBMI1").prop("checked", false);
    });
    $("#CountryMaster_Id").change(function () {
        DropBindingStateList();
        let CTID = $("#CountryMaster_Id option:selected").val();
        if (parseInt(CTID) == 2) {
            //india selected
            $("#MSMEdiv").show();
            //Enable
            $("#PAN").prop("disabled", false);
            $("#GST").prop("disabled", false);
            $("#PinCode").prop("disabled", false);
            $("#MobileNo").prop("disabled", false);
            $("#MobileNo2").prop("disabled", false);
            $("#StateMaster_Id").show();
            $("#StateName").hide(); 
        }
        else {

            $("#MSMEdiv").hide();
            $("#StateMaster_Id").hide();
            $("#StateName").show();
            $('#PAN').siblings('span.error').css('visibility', 'hidden');
            $('#PAN').siblings('span.valid').css('visibility', 'hidden');
            $('#GST').siblings('span.error').css('visibility', 'hidden');
            $('#GST').siblings('span.valid').css('visibility', 'hidden');
            $('#PinCode').siblings('span.error').css('visibility', 'hidden');
            $('#PinCode').siblings('span.valid').css('visibility', 'hidden');
            $('#MobileNo').siblings('span.error').css('visibility', 'hidden');
            $('#MobileNo').siblings('span.valid').css('visibility', 'hidden');
            $('#MobileNo2').siblings('span.error').css('visibility', 'hidden');
            $('#MobileNo2').siblings('span.valid').css('visibility', 'hidden');

            //Disable
            $("#PAN").prop("disabled", true);
            $("#GST").prop("disabled", true);
            $("#PinCode").prop("disabled", true);
            $("#MobileNo").prop("disabled", true);
            $("#MobileNo2").prop("disabled", true);

        }
    });
    $("#TypeBusCB6").click(function () {
        debugger
        var TypeCBVal = $('#TypeBusCB6:checked').val();
        if (TypeCBVal == "true") {
            $("#OthrText").prop("disabled", false);
        }
        else {
            $("#OthrText").prop("disabled", true);
        }
    });
    $("#CDShpFrm1").click(function () {
        var CDShpVal = $('#CDShpFrm1:checked').val();
        if (CDShpVal == "true") {
            let ERpAddVal = $("#ERPAdd1").val();
            let ERPCityVal = $("#ERPCity1").val();
            let ERPStateVal = $("#ERPState1").val();
            let ERPCountryVal = $("#ERPCountry1").val();
            let ERPPinCodeVal = $("#ERPPinCode1").val();
            let ERPPhoneNoVal = $("#ERPPhoneNo1").val();
            let ERPFaxNoVal = $("#ERPFaxNo1").val();
            let ERPMobNoVal = $("#ERPMobNo1").val();
            let ERPEmailIdVal = $("#ERPEmailId1").val();
            let ERPGSTVal = $("#ERPGST1").val();
            $("#ERPAdd2").val(ERpAddVal);
            $("#ERPCity2").val(ERPCityVal);
            $("#ERPState2").val(ERPStateVal);
            $("#ERPCountry2").val(ERPCountryVal);
            $("#ERPPinCode2").val(ERPPinCodeVal);
            $("#ERPPhoneNo2").val(ERPPhoneNoVal);
            $("#ERPFaxNo2").val(ERPFaxNoVal);
            $("#ERPMobNo2").val(ERPMobNoVal);
            $("#ERPEmailId2").val(ERPEmailIdVal);
            $("#ERPGST2").val(ERPGSTVal);

            $("#ERPAdd3").val(ERpAddVal);
            $("#ERPCity3").val(ERPCityVal);
            $("#ERPState3").val(ERPStateVal);
            $("#ERPCountry3").val(ERPCountryVal);
            $("#ERPPinCode3").val(ERPPinCodeVal);
            $("#ERPPhoneNo3").val(ERPPhoneNoVal);
            $("#ERPFaxNo3").val(ERPFaxNoVal);
            $("#ERPMobNo3").val(ERPMobNoVal);
            $("#ERPEmailId3").val(ERPEmailIdVal);
            $("#ERPGST3").val(ERPGSTVal);
        }
        else {
            $("#ERPAdd2").val("");
            $("#ERPCity2").val("");
            $("#ERPState2").val("");
            $("#ERPCountry2").val("");
            $("#ERPPinCode2").val("");
            $("#ERPPhoneNo2").val("");
            $("#ERPFaxNo2").val("");
            $("#ERPMobNo2").val("");
            $("#ERPEmailId2").val("");
            $("#ERPGST2").val("");
            $("#ERPAdd3").val("");
            $("#ERPCity3").val("");
            $("#ERPState3").val("");
            $("#ERPCountry3").val("");
            $("#ERPPinCode3").val("");
            $("#ERPPhoneNo3").val("");
            $("#ERPFaxNo3").val("");
            $("#ERPMobNo3").val("");
            $("#ERPEmailId3").val("");
            $("#ERPGST3").val("");
        }
    });

    //Prev Next
    $('#btn_Nxt1').click(function () {
        $('.nav-tabs > .nav-item > .active').parent().next('li').find('a').trigger('click');
    });
    $('#btn_Nxt2').click(function () {
        $('.nav-tabs > .nav-item > .active').parent().next('li').find('a').trigger('click');
    });
    $('#btn_Nxt3').click(function () {
        $('.nav-tabs > .nav-item > .active').parent().next('li').find('a').trigger('click');
    });
    $('#btn_Nxt4').click(function () {
        $('.nav-tabs > .nav-item > .active').parent().next('li').find('a').trigger('click');
    });
    $('#btn_PreV1').click(function () {
        $('.nav-tabs > .nav-item > .active').parent().prev('li').find('a').trigger('click');
    });
    $('#btn_PreV2').click(function () {
        $('.nav-tabs > .nav-item > .active').parent().prev('li').find('a').trigger('click');
    });
    $('#btn_PreV3').click(function () {
        $('.nav-tabs > .nav-item > .active').parent().prev('li').find('a').trigger('click');
    });
    $('#btn_PreV4').click(function () {
        $('.nav-tabs > .nav-item > .active').parent().prev('li').find('a').trigger('click');
    });

    //On input change event
    $('#EmailId1').on('input', function (e) {
        debugger
        let email1 = $('#EmailId1').val();
        let country = $("#CountryMaster_Id option:selected").val();
        if (country == parseInt(2)) {
            //india selected
            if (email1.length > 0) {
                var filter = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;
                if (!filter.test(email1)) {
                    $('#EmailId1').siblings('span.valid').css('visibility', 'visible');
                    $('#EmailId1').siblings('span.error').css('visibility', 'hidden');
                }
                else {
                    $('#EmailId1').siblings('span.valid').css('visibility', 'hidden');
                }
            }
            else {
                $('#EmailId1').siblings('span.valid').css('visibility', 'hidden');
            }
        }
        else {
            if (email1.length > 0) {
                var filter = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;
                if (!filter.test(email1)) {
                    $('#EmailId1').siblings('span.valid').css('visibility', 'visible');
                    $('#EmailId1').siblings('span.error').css('visibility', 'hidden');
                }
                else {
                    $('#EmailId1').siblings('span.valid').css('visibility', 'hidden');
                }
            }
            else {
                $('#EmailId1').siblings('span.valid').css('visibility', 'hidden');
            }
        }
    });
    $('#EmailId').on('input', function (e) {
        debugger
        let email1 = $('#EmailId').val();
        let country = $("#CountryMaster_Id option:selected").val();
        if (country == parseInt(2)) {
            //india selected
            if (email1.length > 0) {
                var filter = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;
                if (!filter.test(email1)) {
                    $('#EmailId').siblings('span.valid').css('visibility', 'visible');
                    $('#EmailId').siblings('span.error').css('visibility', 'hidden');
                }
                else {
                    $('#EmailId').siblings('span.valid').css('visibility', 'hidden');
                }
            }
            else {
                $('#EmailId').siblings('span.valid').css('visibility', 'hidden');
            }
        }
        else {
            if (email1.length > 0) {
                var filter = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;
                if (!filter.test(email1)) {
                    $('#EmailId').siblings('span.valid').css('visibility', 'visible');
                }
                else {
                    $('#EmailId').siblings('span.valid').css('visibility', 'hidden');
                }
            }
            else {
                $('#EmailId').siblings('span.valid').css('visibility', 'hidden');
            }
        }
    });
    $('#PAN').on('input', function (e) {
        debugger
        let PAN = $('#PAN').val();
        let country = $("#CountryMaster_Id option:selected").val();
        if (country == parseInt(2)) {
            //india selected
            if (PAN.length > 10 || PAN.length < 10) {
                $('#PAN').siblings('span.valid').css('visibility', 'visible');
                $('#PAN').siblings('span.error').css('visibility', 'hidden');
            }
            else {
                $('#PAN').siblings('span.error').css('visibility', 'hidden');
                $('#PAN').siblings('span.valid').css('visibility', 'hidden');
            }
        }



    })
    $('#GST').on('input', function (e) {
        debugger
        let GST = $('#GST').val();
        let country = $("#CountryMaster_Id option:selected").val();
        if (country == parseInt(2)) {
            //india selected
            if (GST.length > 15 || GST.length < 15) {
                $('#GST').siblings('span.valid').css('visibility', 'visible');
                $('#GST').siblings('span.error').css('visibility', 'hidden');
            }
            else {
                $('#GST').siblings('span.error').css('visibility', 'hidden');
                $('#GST').siblings('span.valid').css('visibility', 'hidden');
            }
        }



    })
    $('#PinCode').on('input', function (e) {
        debugger
        let PinCode = $('#PinCode').val();
        let country = $("#CountryMaster_Id option:selected").val();
        if (country == parseInt(2)) {
            //india selected
            if (PinCode.length > 6 || PinCode.length < 6) {
                $('#PinCode').siblings('span.valid').css('visibility', 'visible');
                $('#PinCode').siblings('span.error').css('visibility', 'hidden');
            }
            else {
                $('#PinCode').siblings('span.error').css('visibility', 'hidden');
                $('#PinCode').siblings('span.valid').css('visibility', 'hidden');
            }
        }
    })
    $('#MobileNo').on('input', function (e) {
        debugger
        let MobileNo = $('#MobileNo').val();
        let country = $("#CountryMaster_Id option:selected").val();
        if (country == parseInt(2)) {
            //india selected
            if (MobileNo.length > 10 || MobileNo.length < 10) {
                $('#MobileNo').siblings('span.valid').css('visibility', 'visible');
                $('#MobileNo').siblings('span.error').css('visibility', 'hidden');
            }
            else {
                $('#MobileNo').siblings('span.error').css('visibility', 'hidden');
                $('#MobileNo').siblings('span.valid').css('visibility', 'hidden');
            }
        }
    })
    $('#MobileNo2').on('input', function (e) {
        debugger
        let MobileNo2 = $('#MobileNo2').val();
        let country = $("#CountryMaster_Id option:selected").val();
        if (country == parseInt(2)) {
            //india selected
            if (MobileNo2.length > 10 || MobileNo2.length < 10) {
                $('#MobileNo2').siblings('span.valid').css('visibility', 'visible');
                $('#MobileNo2').siblings('span.error').css('visibility', 'hidden');
            }
            else {
                $('#MobileNo2').siblings('span.error').css('visibility', 'hidden');
                $('#MobileNo2').siblings('span.valid').css('visibility', 'hidden');
            }
        }
    })

    $('#btnSubmit').click(function () {
        debugger
        let isAllValid = true;
        let CountryVal = $("#CountryMaster_Id option:selected").val();
        if (CountryVal == parseInt(2)) {
            if ($("#TypeFirmCB1").get(0).checked || $("#TypeFirmCB2").get(0).checked || $("#TypeFirmCB3").get(0).checked || $("#TypeFirmCB4").get(0).checked || $("#TypeFirmCB5").get(0).checked) {
                $('#lblChk').siblings('span.error').css('visibility', 'hidden');
            }
            else {
                isAllValid = false;
                $('#lblChk').siblings('span.error').css('visibility', 'visible');
            }
            if ($('#PinCode').val() == "") {
                isAllValid = false;
                $('#PinCode').siblings('span.error').css('visibility', 'visible');
            }
            else {
                var pat1 = /^\d{6}$/;
                var PinCode = $('#PinCode').val();
                if (!pat1.test(PinCode)) {
                    isAllValid = false;
                    $('#PinCode').siblings('span.error').css('visibility', 'hidden');
                    $('#PinCode').siblings('span.valid').css('visibility', 'visible');
                }
                else {

                    $('#PinCode').siblings('span.error').css('visibility', 'hidden');
                    $('#PinCode').siblings('span.valid').css('visibility', 'hidden');
                }
            }
            if ($('#MobileNo2').val() == "") {
                isAllValid = false;
                $('#MobileNo2').siblings('span.error').css('visibility', 'visible');
            }
            else {

                var MnVal = $('#MobileNo2').val().length;
                if (MnVal != 10) {
                    isAllValid = false;
                    $('#MobileNo2').siblings('span.error').css('visibility', 'hidden');
                    $('#MobileNo2').siblings('span.valid').css('visibility', 'visible');
                }
                else {
                    $('#MobileNo2').siblings('span.error').css('visibility', 'hidden');
                    $('#MobileNo2').siblings('span.valid').css('visibility', 'hidden');
                }

            }
            if ($('#MobileNo').val() == "") {
                isAllValid = false;
                $('#MobileNo').siblings('span.error').css('visibility', 'visible');
            }
            else {
                var MnVal = $('#MobileNo').val().length;
                if (MnVal != 10) {
                    isAllValid = false;
                    $('#MobileNo').siblings('span.error').css('visibility', 'hidden');
                    $('#MobileNo').siblings('span.valid').css('visibility', 'visible');
                }
                else {
                    $('#MobileNo').siblings('span.error').css('visibility', 'hidden');
                    $('#MobileNo').siblings('span.valid').css('visibility', 'hidden');
                }
            }
            if ($('#EmailId1').val() == "") {
                isAllValid = false;
                $('#EmailId1').siblings('span.error').css('visibility', 'visible');
            }
            else {
                var filter = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;
                var EmIVl = $('#EmailId1').val();
                if (!filter.test(EmIVl)) {
                    isAllValid = false;
                    $('#EmailId1').siblings('span.error').css('visibility', 'hidden');
                    $('#EmailId1').siblings('span.valid').css('visibility', 'visible');
                }
                else {
                    $('#EmailId1').siblings('span.error').css('visibility', 'hidden');
                    $('#EmailId1').siblings('span.valid').css('visibility', 'hidden');
                }
            }
            if ($('#EmailId').val() == "") {
                isAllValid = false;
                $('#EmailId').siblings('span.error').css('visibility', 'visible');
            }
            else {
                var filter = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;
                var EmIVl = $('#EmailId').val();
                if (!filter.test(EmIVl)) {
                    isAllValid = false;
                    $('#EmailId').siblings('span.error').css('visibility', 'hidden');
                    $('#EmailId').siblings('span.valid').css('visibility', 'visible');
                }
                else {
                    $('#EmailId').siblings('span.error').css('visibility', 'hidden');
                    $('#EmailId').siblings('span.valid').css('visibility', 'hidden');
                }
            }
            if ($('#PAN').val() == "") {
                isAllValid = false;
                $('#PAN').siblings('span.error').css('visibility', 'visible');
            }
            else {
                var panVal = $('#PAN').val();
                var regpan = /^([a-zA-Z]){5}([0-9]){4}([a-zA-Z]){1}?$/;
                if (!regpan.test(panVal)) {
                    isAllValid = false;
                    $('#PAN').siblings('span.error').css('visibility', 'hidden');
                    $('#PAN').siblings('span.valid').css('visibility', 'visible');
                } else {
                    $('#PAN').siblings('span.error').css('visibility', 'hidden');
                    $('#PAN').siblings('span.valid').css('visibility', 'hidden');
                }
            }
            if ($('#GST').val() == "") {
                isAllValid = false;
                $('#GST').siblings('span.error').css('visibility', 'visible');
            }
            else {
                var gstVal = $('#GST').val();
                var reggst = /^[0-9]{2}[A-Z]{5}[0-9]{4}[A-Z]{1}[1-9A-Z]{1}Z[0-9A-Z]{1}$/;
                if (!reggst.test(gstVal)) {
                    isAllValid = false;
                    $('#GST').siblings('span.valid').css('visibility', 'visible');
                    $('#GST').siblings('span.error').css('visibility', 'hidden');
                }
                else {
                    $('#GST').siblings('span.error').css('visibility', 'hidden');
                    $('#GST').siblings('span.valid').css('visibility', 'hidden');
                }
            }           
        }
        else {
            if ($("#TypeFirmCB1").get(0).checked || $("#TypeFirmCB2").get(0).checked || $("#TypeFirmCB3").get(0).checked || $("#TypeFirmCB4").get(0).checked || $("#TypeFirmCB5").get(0).checked) {
                $('#lblChk').siblings('span.error').css('visibility', 'hidden');
                isAllValid = true;
            }
            else {
                isAllValid = false;
                $('#lblChk').siblings('span.error').css('visibility', 'visible');
            }
        }
        if (isAllValid) {
            $('#Vendform').submit();
        }
        else {
            if (erList != "") {
                alert(erList);
            }
            else {
                erList = "Something missing! Please check.";
                alert(erList);
            }

            return false;
        }
    });
    $("#Vendform").on("submit", function (event) {
        debugger
        event.preventDefault();
        $('#btnSubmit').attr('disabled', 'disabled');
        var url = $(this).attr("action");
        var formData = $(this).serialize();
        $.ajax({
            url: url,
            type: "POST",
            data: formData,
            dataType: "json",
            success: function (response) {
                alert('Success! ' + JSON.stringify(response));
            },
            error: function (response) {
                alert('Error!');
            },
            complete: function () {
                $('#btnSubmit').removeAttr('disabled');
            }
        })
    });
    CheckMSME();
   
});
function CheckMSME() {
    let CTID = $("#CountryMaster_Id option:selected").val();
    if (parseInt(CTID) == 2) {
        $("#MSMEdiv").show();
    }
    else
    {
        $("#MSMEdiv").hide();
    }
}
function DropBindingStateList() {
    let CounTID = $("#CountryMaster_Id option:selected").val();
    $.get("/VendorMaster/GetStateListData", { CounTID: CounTID },
        function (result) {
            $("#StateMaster_Id").empty().append('<option selected="selected" value="0">-Select-</option>');
            $.each(result, function (index, row) {
                $("#StateMaster_Id").append("<option value='" + row.Value + "'>" + row.Text + "</option>");
            });
        });
    $("#StateMaster_Id").ready();
};
function Add() {
    //validation
    var isAllValid = true;
    if ($('#DocumentList').val() == "0" || $('#DocumentList').val() == "") {
        isAllValid = false;
        $('#DocumentList').siblings('span.error').css('visibility', 'visible');
    }
    else {
        $('#DocumentList').siblings('span.error').css('visibility', 'hidden');
    }
    if ($('#FileUpload').val() == "0" || $('#FileUpload').val() == "") {
        isAllValid = false;
        $('#FileUpload').siblings('span.error').css('visibility', 'visible');
    }
    else {
        $('#FileUpload').siblings('span.error').css('visibility', 'hidden');
    }
    if (isAllValid)
    {
        var DocText = $("#DocumentList option:selected").text(),
            DocVal = $("#DocumentList option:selected").val(),
            DocPathFile = $("#FileUpload").get(0).files,
            DocPath = $("#FileUpload").val(),
        detailsTableBody = $("#tblDocSupp tbody");
        var ListItem = '<tr id="SuppDoc_' + counter + '"><td></td><td>' + (counter + 1) + '</td><td>' + DocText + '</td><td>' + DocPath + '</td><td><a data-itemId="0" href = "#" class="btn btn-sm btn-outline-danger deleteItem">Remove</a><input type="hidden" name="SuppDocRel[' + counter + '].DocName" value="' + DocVal + '" /><input type="hidden" name="SuppDocRel[' + counter + '].DocPath" value="' + DocPath + '" /><input type="hidden" name="SuppDocRel[' + counter + '].FileUpload" value="' + DocPathFile + '" /><input type="hidden" name="SuppDocRel[' + counter + '].IsDelete" value="false" id=SuppDocRel_' + counter + '_IsDeleteC /></td></tr>';
        counter++;
        ClearDSuppDoc();
        detailsTableBody.append(ListItem);
    }
};
function AddMD() {
    var isAllValid = true;
    if ($('#CodeList').val() == "0" || $('#CodeList').val() == "") {
        isAllValid = false;
        $('#CodeList').siblings('span.error').css('visibility', 'visible');
    }
    else {
        $('#CodeList').siblings('span.error').css('visibility', 'hidden');
    }
    if ($('#PDI').val() == "0" || $('#PDI').val() == "") {
        isAllValid = false;
        $('#PDI').siblings('span.error').css('visibility', 'visible');
    }
    else {
        $('#PDI').siblings('span.error').css('visibility', 'hidden');
    }
    if ($('#Size').val() == "0" || $('#Size').val() == "") {
        isAllValid = false;
        $('#Size').siblings('span.error').css('visibility', 'visible');
    }
    else {
        $('#Size').siblings('span.error').css('visibility', 'hidden');
    }
    if ($('#Grade').val() == "0" || $('#Grade').val() == "") {
        isAllValid = false;
        $('#Grade').siblings('span.error').css('visibility', 'visible');
    }
    else {
        $('#Grade').siblings('span.error').css('visibility', 'hidden');
    }
    if (isAllValid) {

        let CodeListVal = $("#CodeList option:selected").val().trim(),
            CodeListText = $("#CodeList option:selected").text().trim(),
            PDIVal = $("#PDI").val(),
            SizeVal = $("#Size").val(),
            GradeVal = $("#Grade").val()
        detailsTableBody = $("#tblSuppMD tbody");

        let ListItem = '<tr id="tblSuppMD_' + counterMD + '"><td></td><td>' + (counterMD + 1) + '</td><td>' + CodeListText + '</td><td>' + PDIVal + '</td><td>' + SizeVal + '</td><td>' + GradeVal + '</td><td><a data-itemId="0" href="JavaScript:void(0);"  class="btn btn-sm btn-outline-danger deleteItem2" data-deleteId ="' + counterMD + '">Remove</a><input type="hidden" name="SupRelMDTrs[' + counterMD + '].CodeList" value="' + CodeListText + '"  /><input type="hidden" name="SupRelMDTrs[' + counterMD + '].PDI" value="' + PDIVal + '"  /><input type="hidden" name="SupRelMDTrs[' + counterMD + '].Size" value="' + SizeVal + '"  /><input type="hidden" name="SupRelMDTrs[' + counterMD + '].Grade" value="' + GradeVal + '"  /><input type="hidden" name="SupRelMDTrs[' + counterMD + '].IsDelete" value="false" id=SupRelMDTrs_' + counterMD + '_IsDeleteC /></td></tr>';
        counterMD++;
        cleardata();
        detailsTableBody.append(ListItem);
    }
};
$(document).on('click', 'a.deleteItem2', function (e)
{
    if (confirm("Are you sure want to remove this record!"))
    {
        let $self = $(this);
        if ($(this).attr('data-itemId') == "0")
        {
            let indexcount = $(this).attr('data-deleteId');
            $(this).parents('tr').css("background-color", "#ff6347").fadeOut(800, function ()
            {
                $(this).hide();
                $("#SupRelMDTrs_" + indexcount + "_IsDeleteC").val(true);
            });
        }
    }
    else {
        e.preventDefault();
    }
});
$(document).on('click', 'a.deleteItem', function (e)
{
    if (confirm("Are you sure want to remove this record!"))
    {
        let $self = $(this);
        if ($(this).attr('data-itemId') == "0")
        {
            let indexcount = $(this).attr('data-deleteId');
            $(this).parents('tr').css("background-color", "#ff6347").fadeOut(800, function ()
            {
                $(this).hide();
                $("#SuppDocRel_" + indexcount + "_IsDeleteC").val(true);
            });
        }
    }
    else {
        e.preventDefault();
    }
});
function cleardata() {
    $("#PDI").val("");
    $("#Size").val("");
    $("#Grade").val("");
    //$('#Line_Number')[0].selectedIndex = 0;
    $("#CodeList").val(0).trigger("chosen:updated");
}
function ClearDSuppDoc()
{
    $("#FileUpload").val("");
    $("#DocumentList").val(0).trigger("chosen:updated");
}
function FnRemoveSD(counterMD) {
    $("#tblSuppMD_" + counterMD).hide();
    $("#IsDeleteC_" + counterMD).val("true");
}