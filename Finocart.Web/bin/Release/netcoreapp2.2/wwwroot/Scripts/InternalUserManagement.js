var SumValueToSplit = "";

$(document).ready(function () {

//Start == Hide error controls on load and keyup event
    $('#lblErrDiffSO').hide();
    $('#lblErrMerge').hide();
    $('#lblErrMergeSelect').hide();
    $('#lblErrSplit').hide();
    $('#lblErrSplitMore').hide();
    $('#lblErrCreateSO').hide();

    $("#Material").keyup(function () {
        $('#lblErrMatcodeMerge').hide();
    });
    $("#Settings").keyup(function () {
        $('#lblErrSettingsBlankMerge').hide();
    });
    $("#MatDesc").keyup(function () {
        $('#lblErrMatDescMerge').hide();
    });
    $("#TotalTolerance").keyup(function () {
        $('#lblErrTotalTolerance0Merge').hide();
        $('#lblErrTotalToleranceBlankMerge').hide();
    });
    $("#Pointer").keyup(function () {
        //;
        $('#lblErrPointer0Merge').hide();
        $('#lblErrPointerBlankMerge').hide();
    });
    $("#PcsUnit").keyup(function () {
        $('#lblErrPcs0Merge').hide();
        $('#lblErrPcsBlankMerge').hide();
        $('#lblErrPCsMatchMerge').hide();
    });
    $("#MMSize").keyup(function () {
        $('#lblErrMMSize0Mege').hide();
        $('#lblErrMMSizeBlankMerge').hide();
    });
    $("#TotalUnits").keyup(function () {
        $('#lblErrTotalUnitsBlankMerge').hide();

    });
    $("#PoDetails").keyup(function () {
        $('#lblErrPOSize0Merge').hide();
        $('#lblErrPOSizeBlankMerge').hide();
    });
    //End == Hide error controls on load and keyup event.
   
    //Display orderist grid.
    OrderListGrid();

    //Display merge details.
    DisplayMergeDetails();

    //Display Split Details();
    DisplaySplitNewDetails();

    //Split added row.
    SplitAddRow();

    //Display created SO details.
    DisplayCreateSODetails();

    //Save merge details.
    SaveMergeDetails();

    //Save split details.--not in use
    SaveSplitDetails();

    //Save Created SO Details details.
    SaveCreateSODetails();

    //Save Split  details.
    SaveSplitNewDetails();
     //Autocomplete of SO
    SOAutoComplete();

    //validation before functioning.
    Validation();
     //Export records in excel.
    ExportToExcel();
});
//Display orderist grid
function OrderListGrid() {
    jQuery("#tblOrderList").jqGrid({
        url: GetOrderList,
        datatype: "json",
        //rowNum: 10,
        rowList: [10, 20, 30],
        height: 'auto',
        // width: '100%',
        //autowidth: true,
        //sortname: 'SONo',
        shrinkToFit: false,
        sortorder: "desc",
        colNames: ['Name', 'Email', 'Mobile Number', 'Designation', 'Role Access', 'Created At', 'Actions'],
        colModel: [
                    //{ name: 'act', index: 'act', width: 25 },
                    { name: 'Name', index: 'Name', sortable: true, width: 70 },
                    { name: 'Email', index: 'Email', sortable: true, width: 100 },
                     { name: 'MobileNumber', index: 'MobileNumber', sortable: true, width: 80 },
                     { name: 'Designation', index: 'Designation', sortable: true, width: 100 },
                     { name: 'Role Access', index: 'Role Access', sortable: true, width: 200 },
                     { name: 'Created At', index: 'Created At', sortable: true, width: 100 },
                    { name: 'Actions', index: 'Actions', sortable: true, width: 70 },

        ],
        pager: '#pager',
        pgbuttons: true,
        viewrecords: true,
        //multiselect: true,
        caption: "Order List",
        loadonce: true,

        //groupingView: {
        //    plusicon: "ui-icon-plus",
        //    minusicon: "ui-icon-minus",
        //    groupField: ["ProductName"],
        //    groupCollapse: true
        //},
        gridComplete: function () {

            //jQuery('#txtSearch').keyup(function () {
            //    //;
            //    var colNames = $("#tblOrderList").jqGrid('getGridParam', 'colModel');

            //    var col = "";

            //    var searchString = jQuery(this).val().toLowerCase()
            //    for (var i = 0; i < colNames.length; i++) {
            //        col = colNames[i].name
            //        searchColumn = jQuery("#tblOrderList").jqGrid('getCol', col, true)

            //        jQuery.each(searchColumn, function () {
            //            if (this.value.toLowerCase().indexOf(searchString) == -1) {
            //                jQuery('#' + this.id).hide()
            //            } else {
            //                jQuery('#' + this.id).show()
            //            }
            //        })
            //    }
            //})


            jQuery('#txtSearch').keyup(function () {
                //;

                searchColumn = jQuery("#tblOrderList").jqGrid('getCol', 'SONo', true)

                //;
                var searchString = jQuery(this).val().toLowerCase()
                jQuery.each(searchColumn, function () {
                    if (this.value.toLowerCase().indexOf(searchString) == -1) {
                        jQuery('#' + this.id).hide()
                    } else {
                        jQuery('#' + this.id).show()
                    }
                })
            })

            var $grid = $("#tblOrderList");

            var subGridOptions = $grid.jqGrid("getGridParam", "subGridOptions"),
                plusIcon = "ui-icon-plus",
                minusIcon = "ui-icon-minus",
                expandAllTitle = "Expand all subgrids",
                collapseAllTitle = "Collapse all subgrids";

            $("#jqgh_" + $grid[0].id + "_subgrid")
                .html('<a style="cursor: pointer;"><span class="ui-icon ' + plusIcon +
                      '" title="' + expandAllTitle + '"></span></a>')
                .click(function () {
                    var $spanIcon = $(this).find(">a>span"),
                        $body = $(this).closest(".ui-jqgrid-view")
                            .find(">.ui-jqgrid-bdiv>div>.ui-jqgrid-btable>tbody");
                    if ($spanIcon.hasClass(plusIcon)) {
                        $spanIcon.removeClass(plusIcon)
                            .addClass(minusIcon)
                            .attr("title", collapseAllTitle);
                        $body.find(">tr.jqgrow>td.sgcollapsed")
                            .click();
                    } else {
                        $spanIcon.removeClass(minusIcon)
                            .addClass(plusIcon)
                            .attr("title", expandAllTitle);
                        $body.find(">tr.jqgrow>td.sgexpanded")
                            .click();
                    }
                })


        },
        //loadComplete: function () {
        //    var myGrid = $("#tblOrderList"); $("#cb_" + myGrid[0].id).hide();
        //},
        //editurl: SaveData,
        subGrid: true,
        subGridOptions: {
            plusicon: "ui-icon-plus",
            minusicon: "ui-icon-minus",
            openicon: "ui-icon-carat-1-sw",
            //expandOnLoad: true,
            //selectOnExpand: true,
            //reloadOnExpand: true,

        },
        subGridRowExpanded: function (subgrid_id, row_id) {
            //;
            var rowData = $(this).getRowData(row_id);
            var SONo = rowData.SONo;

            var subgrid_table_id;
            subgrid_table_id = subgrid_id + "_t";
            var pagerId = "jqGridPager_" + subgrid_id;
            jQuery("#" + subgrid_id).html("<table id='" + subgrid_table_id + "' class='scroll'></table><div id='" + pagerId + "'></div>");
            jQuery("#" + subgrid_table_id).jqGrid({
                url: GetOrderListDetails,
                datatype: "json",
                postData: {
                    'SONo': SONo,
                },
                //  rowNum: 10,
                //  rowList: [10, 20, 30],
                height: 'auto',
                //autowidth: true,
                width: 999,
                //rowList: [10, 20, 30],     , formatter: FirstNameFormatter , formatter: LastNameFormatter  , formatter: SalaryFormatter
                //sortname: 'SONo',
                shrinkToFit: false,
                sortorder: "desc",
                colNames: ['ID', 'PONo', 'SONo', 'Item', 'PO Details', 'Settings', 'Material', 'Material Description', 'MM Size', 'Pointer', 'Pcs Unit', 'Total Tolerance', 'Total Units', 'CTS Per Unit', 'Total CTS', 'MaterialGRP', 'NavisionCode', 'CustomerQuality', 'CustPOType', 'SpecialInstruction', 'SpecialInstructions2', 'SpecialInstruction3', 'SalesInstructions', 'CustomerSKU', 'Certification', 'Reference','AccMin','AccMax'],
                colModel: [
                            { name: 'ID', index: 'ID', hidden: true, title: false },
                            { name: 'PONo', index: 'PONo', hidden: true, title: false },
                            { name: 'SONo', index: 'SONo' },
                            { name: 'Item', index: 'Item', hidden: true, sortable: false, width: 50 },
                            { name: 'PoDetails', index: 'PoDetails', sortable: true, hidden: false, width: 100 },
                            { name: 'Settings', index: 'Settings', sortable: false, hidden: false, width: 230 },
                            { name: 'Material', index: 'Material', sortable: false, width: 180 },
                            { name: 'MatDesc', index: 'MatDesc', sortable: true, width: 200 },
                            { name: 'MMSize', index: 'MMSize', sortable: true, width: 100 },
                            { name: 'Pointer', index: 'Pointer', sortable: true, hidden: false, width: 100 },
                            { name: 'PcsUnit', index: 'PcsUnit', sortable: true, hidden: false, width: 100 },
                            { name: 'TotalTolerance', index: 'TotalTolerance', sortable: true, hidden: false, width: 180 },
                            { name: 'TotalUnits', index: 'TotalUnits', sortable: true, hidden: false, width: 130 },
                            { name: 'CTSPerUnit', index: 'CTSPerUnit', sortable: true, hidden: false, width: 130 },
                            { name: 'TotalCTS', index: 'TotalCTS', sortable: true, hidden: false, width: 130 },
                            { name: 'MaterialGRP', index: 'MaterialGRP', hidden: true, sortable: true },
                            { name: 'NavisionCode', index: 'NavisionCode', sortable: true, hidden: true },
                            { name: 'CustomerQuality', index: 'CustomerQuality', sortable: true, hidden: true },
                            { name: 'CustPOType', index: 'CustPOType', sortable: true, hidden: true },
                            { name: 'SpecialInstruction', index: 'SpecialInstruction', sortable: true, hidden: true },
                            { name: 'SpecialInstructions2', index: 'SpecialInstructions2', sortable: true, hidden: true },
                            { name: 'SpecialInstruction3', index: 'SpecialInstruction3', sortable: true, hidden: true },
                            { name: 'SalesInstructions', index: 'SalesInstructions', sortable: true, hidden: true },
                            { name: 'CustomerSKU', index: 'CustomerSKU', sortable: true, hidden: true },
                            { name: 'Certification', index: 'Certification', sortable: true, hidden: true },
                            { name: 'Reference', index: 'Reference', sortable: true, hidden: true },
                            { name: 'AccMin', index: 'AccMin', sortable: true, hidden: false },
                            { name: 'AccMax', index: 'AccMax', sortable: true, hidden: false },
                            //{ name: 'Delete', index: 'Delete', width: 75, sortable: false,hidden:false },

                ],
                pager: '#pager',
                pgbuttons: true,
                viewrecords: true,
                multiselect: true,
                caption: "Order Details List",
                footerrow: true,
                userDataOnFooter: true,
                //gridComplete: function () {
                //    
                //    var ids1 = jQuery("#" + subgrid_table_id).jqGrid('getDataIDs');
                //    var SubGridID = jQuery("#" + subgrid_table_id);
                //    for (var i = 0; i < ids1.length; i++) {
                //        var cl = ids1[i];
                //        //be = "<input style='height:22px;width:20px;' type='button' value='E' onclick=\"jQuery('#" + subgrid_table_id + "').editRow('" + cl + "');\"  />";
                //        //se = "<input style='height:22px;width:20px;' type='button' value='S' onclick=\"jQuery('#" + subgrid_table_id + "').saveRow('" + cl + "');\"  />";
                //        //ce = "<input style='height:22px;width:20px;' type='button' value='C' onclick=\"jQuery('#" + subgrid_table_id + "').restoreRow('" + cl + "');\" />";
                //        de = "<input style='height:22px;width:20px;' type='button' value='D' onclick=\"jQuery('#" + subgrid_table_id + "').delRowData('" + cl + "');\" />";
                //        jQuery("#" + subgrid_table_id).jqGrid('setRowData', ids1[i], { Delete: de });
                //    }

                //},
                loadComplete: function () {
                    var myGrid = $("#" + subgrid_table_id); $("#cb_" + myGrid[0].id).hide();

                    var PcsUnit = myGrid.jqGrid('getCol', 'PcsUnit', false, 'sum');
                    var CTSPerUnit = myGrid.jqGrid('getCol', 'CTSPerUnit', false, 'sum');
                    var TotalCTS = myGrid.jqGrid('getCol', 'TotalCTS', false, 'sum');

                    jQuery(myGrid).jqGrid('footerData', 'set',
                    {
                        Settings: 'Total:',
                        PcsUnit: PcsUnit,
                        CTSPerUnit: CTSPerUnit,
                        TotalCTS: TotalCTS
                    });

                },

                //onSelectRow: function (row_id) {
                //    var rowData = $('#tblCreateSO').jqGrid('getRowData', row_id);


                //    },

            });


        }

    });
}


//Display merge details.
function DisplayMergeDetails() {
    $('#BtnMerge').click(function (e) {
        //;
        var rids = $('#tblOrderList').jqGrid('getDataIDs');
        var subgridNameArray = [];
        var Count = 0; $('#HFOrderDetailsID').val("");
        for (var i = 0; i < rids.length; i++) {
            var nth_row_id = rids[i];
            var subgridName = "#tblOrderList_" + nth_row_id + "_t";
            subgridNameArray[i] = subgridName;
        }
        $('#lblErrSettingsBlankMerge').hide();
        $('#lblErrMatcodeMerge').hide();
        $('#lblErrMatDescMerge').hide();
        $('#lblErrTotalTolerance0Merge').hide();
        $('#lblErrTotalToleranceBlankMerge').hide();
        $('#lblErrPointer0Merge').hide();
        $('#lblErrPointerBlankMerge').hide();
        $('#lblErrPcs0Merge').hide();
        $('#lblErrPcsBlankMerge').hide();
        $('#lblErrMMSize0Mege').hide();
        $('#lblErrMMSizeBlankMerge').hide();
        $('#lblErrPOSize0Merge').hide();
        $('#lblErrPOSizeBlankMerge').hide();
        $('#lblErrTotalUnitsBlankMerge').hide();
        $('#lblErrPCsMatchMerge').hide();

        $('#lblErrDiffSO').hide();
        $('#lblErrMerge').hide();
        $('#lblErrMergeSelect').hide();



        $(subgridNameArray).each(function () {
            //;
            var selectedIDs = 0;
            selectedIDs = $(this.toString()).jqGrid('getGridParam', 'selarrrow');

            if (selectedIDs != undefined && selectedIDs != 0) {
                Count++;

                if (Count > 1) {
                    $('#lblErrDiffSO').show();
                    $('#HFOrderDetailsID').val("");
                    return;
                }

                if (selectedIDs.length == 1 && Count == 1) {
                    //alert("Please select at least 2 rows for merging");
                    $('#lblErrMerge').show();
                    return;
                }

                //if (selectedIDs.length > 2) {
                //    alert("Cannot select more than 2 rows for merging");
                //    return;
                //}
                var OrderDetailID = ""; var MMSize = 0; var TotalUnits = 0; var Pointer = 0; var PcsUnit = 0;

                for (var i = 0; i < selectedIDs.length; i++) {
                    ;
                    result = $(this.toString()).jqGrid('getRowData', selectedIDs[i]);

                    OrderDetailID += result.ID + ",";

                    HFOrderDetailsID = $('#HFOrderDetailsID').val(OrderDetailID);
                    $('#PONo').val(result.PONo);
                    $('#SONo').val(result.SONo);
                    //$('#CustFGStyleNoBuyer').val(result.CustFGStyleNoBuyer);

                    MMSize += parseFloat(result.MMSize);
                    TotalUnits += parseInt(result.TotalUnits);
                    Pointer += parseFloat(result.Pointer);
                    PcsUnit += parseInt(result.PcsUnit);

                    $('#HFMMSize').val(MMSize);
                    $('#HFTotalUnits').val(TotalUnits);
                    $('#HFPointer').val(Pointer);
                    $('#HFPcsUnit').val(PcsUnit);

                    $('#Settings').val(result.Settings);
                    $('#Material').val(result.Material);
                    $('#MatDesc').val(result.MatDesc);

                    $('#TotalTolerance').val(result.TotalTolerance);
                    $('#TotalUnits').val(TotalUnits);
                    $('#Pointer').val(result.Pointer);

                    //   $('#PcsUnit').val(result.PcsUnit);
                    $('#PcsUnit').val(PcsUnit);

                    $('#CTSPerUnit').val(result.CTSPerUnit);
                    $('#TotalCTS').val(result.TotalCTS);

                    $('#MMSize').val(result.MMSize);
                    $('#PoDetails').val(result.PoDetails);

                    $('#AccMin').val(result.AccMin);
                    $('#AccMax').val(result.AccMax);

                }

                $('#CTSPerUnit').val((parseFloat(Pointer) * parseInt($('#PcsUnit').val())).toFixed(2));

                $('#TotalCTS').val((parseFloat($('#CTSPerUnit').val()) * parseInt($('#TotalUnits').val())).toFixed(2));

            }

        });

        if (Count == 0) {
            $('#lblErrMergeSelect').show();
            $('#HFOrderDetailsID').val("");
            return;
        }

        if ($('#HFOrderDetailsID').val() != "") {
            $('#Modal').modal('show');
            $('#Modal').draggable();
            $('#lblErrDiffSO').hide();
            $('#lblErrMerge').hide();
            $('#lblErrMergeSelect').hide();
            $('#lblErrSplit').hide();
            $('#lblErrSplitMore').hide();
            $('#lblErrCreateSO').hide();
        }


    });
}

function DisplaySplitDetails() {
    $('#BtnSplit').click(function (e) {
        //;
        var rids = $('#tblOrderList').jqGrid('getDataIDs');
        var subgridNameArray = [];
        var count = 0; $('#HFOrderDetailsID').val("");
        for (var i = 0; i < rids.length; i++) {
            var nth_row_id = rids[i];
            var subgridName = "#tblOrderList_" + nth_row_id + "_t";
            subgridNameArray[i] = subgridName;
        }

        $(subgridNameArray).each(function () {
            //;
            var selectedIDs = 0;
            selectedIDs = $(this.toString()).jqGrid('getGridParam', 'selarrrow');

            if (selectedIDs != undefined && selectedIDs != 0) {
                count++;

                if (count > 1) {
                    //alert("Cannot select different SONo");
                    $('#lblErrDiffSO').show();
                    $('#HFOrderDetailsID').val("");
                    return;
                }

                if (selectedIDs.length > 1) {
                    //alert("Cannot select more than 1 row for spliting");
                    $('#lblErrSplit').show();
                    return;
                }

                for (var i = 0; i < selectedIDs.length; i++) {

                    var result = $(this.toString()).jqGrid('getRowData', selectedIDs[i]);

                    //OrderDetailID = result.ID;

                    $('#HFOrderDetailsID').val(result.ID);
                    $('#PONo1').val(result.PONo);
                    $('#SONo1').val(result.SONo);

                    $('#HFMMSize').val(result.MMSize);
                    $('#HFTotalUnits').val(result.TotalUnits);
                    $('#HFPointer').val(result.Pointer);
                    $('#HFPcsUnit').val(result.PcsUnit);

                    $('#Settings1').val(result.Settings);
                    $('#Material1').val(result.Material);
                    $('#MatDesc1').val(result.MatDesc);

                    $('#TotalTolerance1').val(result.TotalTolerance);
                    $('#TotalUnits1').val(result.TotalUnits);
                    $('#Pointer1').val(result.Pointer);

                    $('#PcsUnit1').val(result.PcsUnit);
                    $('#CTSPerUnit1').val(result.CTSPerUnit);
                    $('#TotalCTS1').val(result.TotalCTS);
                    $('#MMSize1').val(result.MMSize);
                    $('#PoDetails1').val(result.PoDetails);



                    $('#Settings2').val(result.Settings);
                    $('#Material2').val(result.Material);
                    $('#MatDesc2').val(result.MatDesc);

                    $('#TotalTolerance2').val(result.TotalTolerance);
                    $('#TotalUnits2').val(result.TotalUnits);
                    $('#Pointer2').val(result.Pointer);

                    $('#PcsUnit2').val(result.PcsUnit);
                    $('#CTSPerUnit2').val(result.CTSPerUnit);
                    $('#TotalCTS2').val(result.TotalCTS);
                    $('#MMSize2').val(result.MMSize);
                    $('#PoDetails2').val(result.PoDetails);

                    //$('#MMSize1').val(result.MMSize);
                    //$('#TotalUnits1').val(result.TotalUnits);
                    //$('#Pointer1').val(result.Pointer);
                    //$('#PcsUnit1').val(result.PcsUnit);


                    //$('#MMSize2').val(result.MMSize);
                    //$('#TotalUnits2').val(result.TotalUnits);
                    //$('#Pointer2').val(result.Pointer);
                    //$('#PcsUnit2').val(result.PcsUnit);
                }
            }

        });

        if (count == 0) {
            //alert("Please select 1 row for spliting");
            $('#lblErrSplitMore').show();
            $('#HFOrderDetailsID').val("");
            return;
        }

        if ($('#HFOrderDetailsID').val() != "") {
            $('#ModalSplit').modal('show');
            $('#ModalSplit').draggable();
        }

    });
}
//Display Split Details();
function DisplaySplitNewDetails() {
    $('#BtnSplitNew').click(function (e) {
        //;
        $('#lblErrPCsMatch').hide();
        $('#lblErrTotalUnitsMatch').hide();
        $('#lblErrMMSize0').hide();
        $('#lblErrMMSizeBlank').hide();
        $('#lblErrPointer0').hide();
        $('#lblErrPointerBlank').hide();
        $('#lblErrPcs0').hide();
        $('#lblErrPcsBlank').hide();
        $('#lblErrTotalTolerance0').hide();
        $('#lblErrTotalToleranceBlank').hide();
        $('#lblErrTotalUnitsBlank').hide();
        $('#lblErrSettingsBlank').hide();
        $('#lblErrPOSize0').hide();
        $('#lblErrPOSizeBlank').hide();
        $('#lblErrMatDesc').hide();
        $('#lblErrMatcode').hide();
        // $('#tblsplitnew').jqGrid('setColProp', 'PONo', { editable: false });
        var subgridNameArray = []; var SplitResult = [];
        var Count = 0; $('#HFOrderDetailsID').val("");

        var rids = $('#tblOrderList').jqGrid('getDataIDs');

        for (var i = 0; i < rids.length; i++) {
            var nth_row_id = rids[i];
            var subgridName = "#tblOrderList_" + nth_row_id + "_t";
            subgridNameArray[i] = subgridName;
        }

        $(subgridNameArray).each(function () {
            ////;
            var selectedIDs = 0;
            selectedIDs = $(this.toString()).jqGrid('getGridParam', 'selarrrow');

            if (selectedIDs != undefined && selectedIDs != 0) {
                Count++;

                if (Count > 1) {
                    $('#lblErrDiffSO').show();
                    $('#HFOrderDetailsID').val("");
                    return;
                }

                if (selectedIDs.length > 1) {
                    //alert("Cannot select more than 1 row for spliting");
                    $('#lblErrSplit').show();
                    return;
                }

                var OrderDetailID = ""; var PONo = ""; var SONo = ""; var HFSplitPcsUnits = "";

                for (var i = 0; i < 2; i++) {
                    //;
                    result = $(this.toString()).jqGrid('getRowData', selectedIDs);

                    OrderDetailID += result.ID + ",";

                    HFOrderDetailsID = $('#HFOrderDetailsID').val(OrderDetailID);
                    PONo = $('#HFPONo').val(result.PONo);
                    SONo = $('#HFSONo').val(result.SONo);
                    HFSplitPcsUnits = $('#HFSplitPcsUnit').val(result.PcsUnit);

                    SplitResult.push($(this.toString()).jqGrid('getRowData', selectedIDs));
                }

            }
        });

        if (Count == 0) {
            //alert("Please select at least one rows for Split");
            $('#lblErrSplitMore').show();
            $('#HFOrderDetailsID').val("");
            return;
        }

        var data = { rows: SplitResult }
        //var data1 = { rows: SOResultForModalPopUp }
        //console.log(SOResultForModalPopUp);
        //jQuery("#tblCreateSO").jqGrid("clearGridData");       

        jQuery('#tblSplitNew').jqGrid('clearGridData').jqGrid('setGridParam', { data: data.rows, datatype: 'local' }).trigger('reloadGrid');

        jQuery("#tblSplitNew").jqGrid({

            //url: GetOrderList,
            data: data.rows,
            datatype: "local",
            //  rowNum: 15,
            height: 'auto',
            //width: 1170,
            //autowidth: true,
            //search:true,
            //rowList: [10, 20, 30],  delRowData  , editable: true editable: true, edittype: 'text',
            //sortname: 'LoginName',
            shrinkToFit: false,
            // sortorder: "desc",
            colNames: ['SONo', 'Item', 'Settings', 'PO Details', 'Material', 'Material Description', 'MM Size', 'Pointer', 'Pcs Unit', 'Acc Min','Acc Max','Total Tolerance', 'Total Units', 'CTS Per Unit', 'Total CTS', 'MaterialGRP', 'NavisionCode', 'CustomerQuality', 'CustPOType', 'SpecialInstruction', 'SpecialInstructions2', 'SpecialInstruction3', 'SalesInstructions', 'CustomerSKU', 'Certification', 'Reference', 'Delete'],
            colModel: [
                        //{ name: 'ID', index: 'ID', hidden: true, title: false },
                        //{ name: 'PONo', index: 'PONo', hidden: true, title: false },
                        { name: 'SONo', index: 'SONo', hidden: true, sortable: true },
                        { name: 'Item', index: 'Item', hidden: true, sortable: false, width: 50 },
                        { name: 'Settings', index: 'Settings', sortable: false, hidden: true, width: 400 },
                        { name: 'PoDetails', index: 'PoDetails', sortable: false, title: false, hidden: true, editable: true, classes:'form-cntrl-adduser' },
                        { name: 'Material', index: 'Material', sortable: false, width: 300, editable: true },
                        { name: 'MatDesc', index: 'MatDesc', sortable: true, width: 400, editable: true },
                        { name: 'MMSize', index: 'MMSize', sortable: true, editable: true },
                        { name: 'Pointer', index: 'Pointer', sortable: true, hidden: false, formatter: TextPointer, sorttype: 'float', formatoptions: { decimalPlaces: 2 } },
                        { name: 'PcsUnit', index: 'PcsUnit', sortable: false, formatter: TextPcsUnit },
                        { name: 'AccMin', index: 'AccMin', sortable: true, hidden: false, width: 250, editable: true },
                        { name: 'AccMax', index: 'AccMax', sortable: true, hidden: false, width: 250, editable: true },
                        { name: 'TotalTolerance', index: 'TotalTolerance', sortable: true, hidden: true, width: 250, editable: true },
                        { name: 'TotalUnits', index: 'TotalUnits', sortable: true, hidden: false, formatter: TextTotalUnits },
                        { name: 'CTSPerUnit', index: 'CTSPerUnit', sortable: true, hidden: false, editable: false, sorttype: 'float', formatoptions: { decimalPlaces: 2 } },
                        { name: 'TotalCTS', index: 'TotalCTS', sortable: true, hidden: false },
                        { name: 'MaterialGRP', index: 'MaterialGRP', hidden: true, sortable: true },
                        { name: 'NavisionCode', index: 'NavisionCode', sortable: true, hidden: true },
                        { name: 'CustomerQuality', index: 'CustomerQuality', sortable: true, hidden: true },
                        { name: 'CustPOType', index: 'CustPOType', sortable: true, hidden: true },
                        { name: 'SpecialInstruction', index: 'SpecialInstruction', sortable: true, hidden: true },
                        { name: 'SpecialInstructions2', index: 'SpecialInstructions2', sortable: true, hidden: true },
                        { name: 'SpecialInstruction3', index: 'SpecialInstruction3', sortable: true, hidden: true },
                        { name: 'SalesInstructions', index: 'SalesInstructions', sortable: true, hidden: true },
                        { name: 'CustomerSKU', index: 'CustomerSKU', sortable: true, hidden: true },
                        { name: 'Certification', index: 'Certification', sortable: true, hidden: true },
                        { name: 'Reference', index: 'Reference', sortable: true, hidden: true },
                        { name: 'Delete', index: 'Delete', width: 75, sortable: false, hidden: false },

            ],
            pager: '#SplitNewpager',
            pgbuttons: true,
            viewrecords: true,
            footerrow: true,
            userDataOnFooter: true,
            gridComplete: function () {
                
                var ids1 = jQuery("#tblSplitNew").jqGrid('getDataIDs');
                var SubGridID = jQuery("#tblSplitNew");
                for (var i = 0; i < ids1.length; i++) {
                    
                    var cl = ids1[i];
                    //be = "<input style='height:22px;width:20px;' type='button' value='E' onclick=\"jQuery('#" + subgrid_table_id + "').editRow('" + cl + "');\"  />";
                    //se = "<input style='height:22px;width:20px;' type='button' value='S' onclick=\"jQuery('#" + subgrid_table_id + "').saveRow('" + cl + "');\"  />";
                    //ce = "<input style='height:22px;width:20px;' type='button' value='C' onclick=\"jQuery('#" + subgrid_table_id + "').restoreRow('" + cl + "');\" />";
                    if (i > 1) {
                        de = "<input style='height:22px;width:20px;' type='button' value='D' onclick=\"jQuery('#tblSplitNew').delRowData('" + cl + "');\" />";
                        jQuery("#tblSplitNew").jqGrid('setRowData', ids1[i], { Delete: de });
                    }

                }

            },
            loadComplete: function () {
                //;
                var $this = jQuery("#tblSplitNew"), ids = $this.jqGrid('getDataIDs'), i, l = ids.length;
                var HFSplitSumPcsUnit = "";
                var PcsUnit = $this.jqGrid('getCol', 'PcsUnit', false, 'sum');
                jQuery($this).jqGrid('footerData', 'set',
                {
                    Settings: 'Total:',
                    PcsUnit: PcsUnit,
                });
                HFSplitSumPcsUnit = $('#HFSplitPcsUnitSum').val(PcsUnit);
                for (i = 0; i < l; i++) {
                    $this.jqGrid('editRow', ids[i], true);
                }
                $footer = $(this).closest(".ui-jqgrid-bdiv").next(".ui-jqgrid-sdiv");
                $footer.hide();

            }
        });

        if ($('#HFOrderDetailsID').val() != "") {
            $('#ModalSplitNew').modal('show');
            $('#ModalSplitNew').draggable();
            $('#lblErrDiffSO').hide();
            $('#lblErrMerge').hide();
            $('#lblErrMergeSelect').hide();
            $('#lblErrSplit').hide();
            $('#lblErrSplitMore').hide();
            $('#lblErrCreateSO').hide();
        }

    });

    //function TextPcsUnit(cellValue, options, rowdata, action) {
    //    if (cellValue == null)
    //        cellValue = "0"
    //    return '<input type="text" class="form-control" name="PcsUnit" value="' + cellValue + '" id="PcsUnit' + options.rowId + '" onchange="SetPcsUnit(this,event);" onkeypress=""/>'
    //}

    function TextPointer(cellvalue, options, rowObject) {
        
        if (rowObject.Pointer == null)
            rowObject.Pointer = "";
        return '<input type="text" class="form-control" id="Pointer' + options.rowId + '" value="' + rowObject.Pointer + '" name="Pointer" value="" onchange="SetPointer(this,event);" onkeypress="" />';
    }

    function TextPcsUnit(cellvalue, options, rowObject) {
        
        if (rowObject.PcsUnit == null)
            rowObject.PcsUnit = "";
        return '<input type="text" class="form-control" id="PcsUnit' + options.rowId + '" value="' + rowObject.PcsUnit + '" name="PcsUnit"  onchange="SetPcsUnit(this,event);" onkeypress="" />';
    }

    function TextTotalUnits(cellvalue, options, rowObject) {
        
        if (rowObject.TotalUnits == null)
            rowObject.TotalUnits = "";
        return '<input type="text" class="form-control" id="TotalUnits' + options.rowId + '" value="' + rowObject.TotalUnits + '" name="TotalUnits" value="" onchange="SetTotalUnits(this,event);" onkeypress="" />';
    }

}



function SetPointer(current, e) {
    
    var errormsg = "";
    var rowId = current.id;
    var rId = rowId.replace('Pointer', '');

    var myGrid = $('#tblSplitNew');

    var Pointer = $('#Pointer' + rId).val();
    if (Pointer != "") {
        if (Pointer == 0) {
            errormsg = errormsg + "Pointer Value can not hold value 0.\n";
        }
    }
    else
    {
        errormsg = errormsg + "Pointer Value can not be blank.\n";
    }
    if (errormsg != "")
    {
        alert(errormsg);
        return false;
    }
    var PcsUnit = $('#PcsUnit' + rId).val();
    var TotalUnit = $('#TotalUnits' + rId).val()

    var CTSPerUnit = parseFloat(Pointer) * parseInt(PcsUnit);
    var TotalCTS = (parseFloat(TotalUnit) * parseFloat(CTSPerUnit)).toFixed(2)

    myGrid.jqGrid('setCell', rId, 'CTSPerUnit', CTSPerUnit);
    myGrid.jqGrid('setCell', rId, 'TotalCTS', TotalCTS);
}

function SetPcsUnit(current, e) {
    var errormsg = "";
    var rowId = current.id;
    var rId = rowId.replace('PcsUnit', '');

    var myGrid = $('#tblSplitNew');
    
    var Pointer = $('#Pointer' + rId).val();
    var PcsUnit = $('#PcsUnit' + rId).val();
    if (PcsUnit != "") {
        if (PcsUnit == 0) {
            errormsg = errormsg + "Pointer Value can not hold value 0.\n";
        }
    }
    else {
        errormsg = errormsg + "Pointer Value can not be blank.\n";
    }
    if (errormsg != "") {
        alert(errormsg);
        return false;
    }
    var TotalUnit = $('#TotalUnits' + rId).val()

    var CTSPerUnit = parseFloat(Pointer) * parseInt(PcsUnit);
    var TotalCTS = (parseFloat(TotalUnit) * parseFloat(CTSPerUnit)).toFixed(2)

    myGrid.jqGrid('setCell', rId, 'CTSPerUnit', CTSPerUnit);
    myGrid.jqGrid('setCell', rId, 'TotalCTS', TotalCTS);
    myGrid.jqGrid('getLocalRow', rId).PcsUnit = PcsUnit;

}

function SetTotalUnits(current, e) {
    var errormsg = "";
    var rowId = current.id;
    var rId = rowId.replace('TotalUnits', '');

    var myGrid = $('#tblSplitNew');

    var Pointer = $('#Pointer' + rId).val();
    var PcsUnit = $('#PcsUnit' + rId).val();
    var TotalUnit = $('#TotalUnits' + rId).val()
    if (TotalUnit != "") {
        if (TotalUnit == 0) {
            errormsg = errormsg + "Pointer Value can not hold value 0.\n";
        }
    }
    else {
        errormsg = errormsg + "Pointer Value can not be blank.\n";
    }
    if (errormsg != "") {
        alert(errormsg);
        return false;
    }
    var CTSPerUnit = parseFloat(Pointer) * parseInt(PcsUnit);
    var TotalCTS = (parseFloat(TotalUnit) * parseFloat(CTSPerUnit)).toFixed(2)

    myGrid.jqGrid('setCell', rId, 'CTSPerUnit', CTSPerUnit);
    myGrid.jqGrid('setCell', rId, 'TotalCTS', TotalCTS);
        
}


function SplitAddRow() {
    $('#BtnAddRow').click(function (e) {
        //;

        var subgridNameArray = []; var SplitResult = [];
        var Count = 0;

        var rids = $('#tblOrderList').jqGrid('getDataIDs');

        for (var i = 0; i < rids.length; i++) {
            var nth_row_id = rids[i];
            var subgridName = "#tblOrderList_" + nth_row_id + "_t";
            subgridNameArray[i] = subgridName;
        }

        $(subgridNameArray).each(function () {
            ////;
            var selectedIDs = 0;
            selectedIDs = $(this.toString()).jqGrid('getGridParam', 'selarrrow');

            if (selectedIDs != undefined && selectedIDs != 0) {
                Count++;

                //var OrderDetailID = ""; var PONo = ""; var SONo = "";

                for (var i = 0; i < selectedIDs.length; i++) {
                    //;
                    result = $(this.toString()).jqGrid('getRowData', selectedIDs[i]);

                    //OrderDetailID += result.ID + ",";

                    //HFOrderDetailsID = $('#HFOrderDetailsID').val(OrderDetailID[i]);
                    //PONo = $('#HFPONo').val(result.PONo);
                    //SONo = $('#HFSONo').val(result.SONo);

                    //var rowId = $.jgrid.randId();
                    //$("#tblSplitNew").jqGrid('addRowData', rowId, selectedIDs[i]);

                    SplitResult.push($(this.toString()).jqGrid('getRowData', selectedIDs[i]));
                }
                var data = { rows: SplitResult }

                jQuery("#tblSplitNew").jqGrid('addRowData', 0, SplitResult, "last");
                jQuery("#tblSplitNew").jqGrid('setGridParam', { datatype: 'local' }).trigger('reloadGrid');
            }
        });

    });

}

function DisplayCreateSODetails() {
    $('#BtnCreateSO').click(function (e) {
        //;
        $('#trCreateSOHeader input[type=radio]:checked').prop('checked', false);
        var subgridNameArray = []; var CreatedSOResult = [];
        var Count = 0; $('#HFOrderDetailsID').val("");

        var rids = $('#tblOrderList').jqGrid('getDataIDs');

        for (var i = 0; i < rids.length; i++) {
            var nth_row_id = rids[i];
            var subgridName = "#tblOrderList_" + nth_row_id + "_t";
            subgridNameArray[i] = subgridName;
        }

        $(subgridNameArray).each(function () {
            ////;
            var selectedIDs = 0;
            selectedIDs = $(this.toString()).jqGrid('getGridParam', 'selarrrow');

            if (selectedIDs != undefined && selectedIDs != 0) {
                Count++;

                if (Count > 1) {
                    // alert("Cannot select different SONo");
                    $('#lblErrDiffSO').show();
                    $('#HFOrderDetailsID').val("");
                    return;
                }
                var OrderDetailID = ""; var PONo = ""; var SONo = "";
                for (var i = 0; i < selectedIDs.length; i++) {
                    //;
                    result = $(this.toString()).jqGrid('getRowData', selectedIDs[i]);

                    OrderDetailID += result.ID + ",";

                    HFOrderDetailsID = $('#HFOrderDetailsID').val(OrderDetailID);
                    PONo = $('#HFPONo').val(result.PONo);
                    SONo = $('#HFSONo').val(result.SONo);

                    CreatedSOResult.push($(this.toString()).jqGrid('getRowData', selectedIDs[i]));
                }

            }
        });

        if (Count == 0) {
            //alert("Please select at least one rows for create so");
            $('#lblErrCreateSO').show();
            $('#HFOrderDetailsID').val("");
            return;
        }

        var data = { rows: CreatedSOResult }
        //var data1 = { rows: SOResultForModalPopUp }
        //console.log(SOResultForModalPopUp);
        //jQuery("#tblCreateSO").jqGrid("clearGridData");       

        jQuery('#tblCreateSO').jqGrid('clearGridData').jqGrid('setGridParam', { data: data.rows, datatype: 'local' }).trigger('reloadGrid');

        jQuery("#tblCreateSO").jqGrid({

            //url: GetOrderList,
            data: data.rows,
            datatype: "local",
            //  rowNum: 15,
            height: 'auto',
            //width: 1170,
            //autowidth: true,
            //search:true,
            //rowList: [10, 20, 30],  delRowData  , editable: true 
            //sortname: 'LoginName',
            shrinkToFit: false,
            // sortorder: "desc",
            colNames: ['ID', 'PONo', 'SONo', 'Item', 'PO Details', 'Settings', 'Material', 'Material Description', 'MM Size', 'Pointer', 'Pcs Unit', 'Total Tolerance', 'Total Units', 'CTS Per Unit', 'Total CTS', 'MaterialGRP', 'NavisionCode', 'CustomerQuality', 'CustPOType', 'SpecialInstruction', 'SpecialInstructions2', 'SpecialInstruction3', 'SalesInstructions', 'CustomerSKU', 'Certification', 'Reference'],
            colModel: [
                        { name: 'ID', index: 'ID', hidden: true, title: false },
                        { name: 'PONo', index: 'PONo', hidden: true, title: false },
                        { name: 'SONo', index: 'SONo', hidden: true, sortable: true },
                        { name: 'Item', index: 'Item', hidden: true, sortable: false, width: 50 },
                        { name: 'PoDetails', index: 'PoDetails', sortable: true, hidden: false },
                        { name: 'Settings', index: 'Settings', sortable: false, hidden: false, width: 400 },
                        { name: 'Material', index: 'Material', sortable: false, width: 300 },
                        { name: 'MatDesc', index: 'MatDesc', sortable: true, width: 400 },
                        { name: 'MMSize', index: 'MMSize', sortable: true },
                        { name: 'Pointer', index: 'Pointer', sortable: true, hidden: false },
                        { name: 'PcsUnit', index: 'PcsUnit', sortable: true, hidden: false },
                        { name: 'TotalTolerance', index: 'TotalTolerance', sortable: true, hidden: false, width: 250 },
                        { name: 'TotalUnits', index: 'TotalUnits', sortable: true, hidden: false },
                        { name: 'CTSPerUnit', index: 'CTSPerUnit', sortable: true, hidden: false },
                        { name: 'TotalCTS', index: 'TotalCTS', sortable: true, hidden: false },
                        { name: 'MaterialGRP', index: 'MaterialGRP', hidden: true, sortable: true },
                        { name: 'NavisionCode', index: 'NavisionCode', sortable: true, hidden: true },
                        { name: 'CustomerQuality', index: 'CustomerQuality', sortable: true, hidden: true },
                        { name: 'CustPOType', index: 'CustPOType', sortable: true, hidden: true },
                        { name: 'SpecialInstruction', index: 'SpecialInstruction', sortable: true, hidden: true },
                        { name: 'SpecialInstructions2', index: 'SpecialInstructions2', sortable: true, hidden: true },
                        { name: 'SpecialInstruction3', index: 'SpecialInstruction3', sortable: true, hidden: true },
                        { name: 'SalesInstructions', index: 'SalesInstructions', sortable: true, hidden: true },
                        { name: 'CustomerSKU', index: 'CustomerSKU', sortable: true, hidden: true },
                        { name: 'Certification', index: 'Certification', sortable: true, hidden: true },
                        { name: 'Reference', index: 'Reference', sortable: true, hidden: true },


            ],
            pager: '#CreateSOpager',
            pgbuttons: true,
            viewrecords: true,
            //loadComplete: function () {
            //    //;                     
            //    var $this = jQuery("#tblCreateSO"), ids = $this.jqGrid('getDataIDs'), i, l = ids.length;
            //    for (i = 0; i < l; i++) {
            //        $this.jqGrid('editRow', ids[i], true);
            //    }
            //}
        });

        if ($('#HFOrderDetailsID').val() != "") {
            $('#ModalCreateSO').modal('show');
            $('#ModalCreateSO').draggable();
        }
    });
}

function SaveMergeDetails() {
    $('#BtnSaveMerge').click(function () {
        //;
        var HFOrderDetailsID = $('#HFOrderDetailsID').val();

        var HFMMSize = $('#HFMMSize').val();
        var HFTotalUnits = $('#HFTotalUnits').val();
        var HFPointer = $('#HFPointer').val();
        var HFPcsUnit = $('#HFPcsUnit').val();

        var MMSize = $('#MMSize').val();
        var TotalUnits = $('#TotalUnits').val();
        var Pointer = $('#Pointer').val();
        var PcsUnit = $('#PcsUnit').val();
        var Settings = $('#Settings').val();
        var MatDesc = $('#MatDesc').val();
        var MatCode = $('#Material').val();
        var PODetails = $('#PoDetails').val();
        var TotalTolerance = $('#TotalTolerance').val();

        var AccMin = $('#AccMin').val();
        var AccMax = $('#AccMax').val();


        var errormsg = "";
        //if (parseFloat(MMSize) != parseFloat(HFMMSize)) {
        //    errormsg = errormsg + "MMSize not match with existing size.\n";            
        //}
        //if (parseInt(TotalUnits) != parseInt(HFTotalUnits)) {
        //    errormsg = errormsg + "Total Units not match with existing Total Units.\n";
        //}
        //if (parseFloat(Pointer) != parseFloat(HFPointer)) {
        //    errormsg = errormsg + "Pointer not match with existing pointer.\n";            
        //}
        if (PcsUnit != "") {
            if (parseInt(PcsUnit) != parseInt(HFPcsUnit)) {
                errormsg = errormsg + "Pcs Unit not match with existing Pcs Unit.\n";
                //$('#lblErrPCsMatchMerge').show();
                //errormsg = true;
            }
        }

        if (TotalTolerance != "") {
            if (TotalTolerance == 0) {
                //$('#lblErrTotalTolerance0Merge').show();
                errormsg = errormsg + "Total Tolerance can not hold value 0.\n";
            }
        }
        else {
            // $('#lblErrTotalToleranceBlankMerge').show();
            errormsg = errormsg + "Total Tolerance can not be left blank.\n";
        }
        if (Settings == "") {
            //$('#lblErrSettingsBlankMerge').show();
            errormsg = errormsg + "Please enter settings.\n";
        }
        if (MatDesc == "") {
            //$('#lblErrMatDescMerge').show();
            errormsg = errormsg + "Please enter diamond description.\n";
        }
        if (MatCode == "") {
            //$('#lblErrMatcodeMerge').show();
            errormsg = errormsg + "Please enter diamond code.\n";
        }
        if (MMSize != "") {
            if (MMSize == 0) {
                //$('#lblErrMMSize0Mege').show();
                errormsg = errormsg + "MM size can not hold value 0.\n";
            }
        }
        else {
            //$('#lblErrMMSizeBlankMerge').show();
            errormsg = errormsg + "MM size can not be left blank.\n";

        }

        if (Pointer != "") {
            if (Pointer == 0) {
                //$('#lblErrPointer0Merge').show();
                errormsg = errormsg + "Pointer can not hold value 0.\n";
            }
            var MaxMatDescValue = 0; MinMatDescValue = 0;
            var separators = ['\\\+', '-'];
            var MatDescValue = MatDesc.split(new RegExp(separators.join('|'), 'g'));
            MaxMatDescValue = MatDescValue[MatDescValue.length - 2].trim();
            MinMatDescValue = MatDescValue[MatDescValue.length - 1].trim();

            if (parseFloat(Pointer) < parseFloat(MaxMatDescValue) || parseFloat(Pointer) > parseFloat(MinMatDescValue)) {
                errormsg = errormsg + "Pointer should be between material description.\n";
            }

        }
        else {
            //$('#lblErrPointerBlankMerge').show();
            errormsg = errormsg + "Pointer can not be left blank.\n";
        }

        if (PcsUnit != "") {
            if (PcsUnit == 0) {
                //$('#lblErrPcs0Merge').show();
                errormsg = errormsg + "PcsUnit can not hold value 0.\n";
            }
        }
        else {
            //$('#lblErrPcs0Merge').show();
            errormsg = errormsg + "PcsUnit can not be left blank.\n";
        }

        if (PODetails != "") {
            if (PODetails == 0) {
                //$('#lblErrPOSize0Merge').show();
                errormsg = errormsg + "PO size can not hold value 0.\n";
            }
        }
        else {
            //$('#lblErrPOSizeBlankMerge').show();
            errormsg = errormsg + "PO size can not br blank.\n";
        }
        if (errormsg != "") {
            alert(errormsg);
            return false;
        }


        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            url: UpdateData,
            data: JSON.stringify({ HFOrderDetailsID: HFOrderDetailsID, MMSize: MMSize, TotalUnits: TotalUnits, Pointer: Pointer, PcsUnit: PcsUnit, AccMin:AccMin, AccMax:AccMax }),
            dataType: "json",
            success: function (data) {
                alert("Data Save Successfully");
                location.reload();
            },
            error: function (result) {
                alert("Error");
            }
        });
    });
}

function SaveSplitDetails() {
    $('#BtnSaveSplit').click(function () {
        //;
        var HFOrderDetailsID = $('#HFOrderDetailsID').val();

        var HFMMSize = $('#HFMMSize').val();
        var HFTotalUnits = $('#HFTotalUnits').val();
        var HFPointer = $('#HFPointer').val();
        var HFPcsUnit = $('#HFPcsUnit').val();

        var MMSize = $('#MMSize1').val();
        var TotalUnits = $('#TotalUnits1').val();
        var Pointer = $('#Pointer1').val();
        var PcsUnit = $('#PcsUnit1').val();

        var MMSizeSplit = $('#MMSize2').val();
        var TotalUnitsSplit = $('#TotalUnits2').val();
        var PointerSplit = $('#Pointer2').val();
        var PcsUnitSplit = $('#PcsUnit2').val();

        var errormsg = "";
        //if ((parseFloat(MMSize) + parseFloat(MMSizeSplit)) != parseFloat(HFMMSize)) {
        //    errormsg = errormsg + "MMSize not match with existing size.\n";
        //}
        if ((parseInt(TotalUnits) + parseInt(TotalUnitsSplit)) != parseInt(HFTotalUnits)) {
            errormsg = errormsg + "TotalUnits not match with existing TotalUnits.\n";

        }
        //if ((parseFloat(Pointer) + parseFloat(PointerSplit)) != parseFloat(HFPointer)) {
        //    errormsg = errormsg + "Pointer not match with existing pointer.\n";
        //}
        if ((parseInt(PcsUnit) + parseInt(PcsUnitSplit)) != parseInt(HFPcsUnit)) {
            errormsg = errormsg + "PcsUnit not match with existing PcsUnit.\n";

        }

        if (errormsg != "") {
            alert(errormsg);
            return false;
        }

        //if ((parseFloat(MMSize) + parseFloat(MMSizeSplit)) != parseFloat(HFMMSize)) {
        //    alert("MMSize not match with existing size");
        //    return;
        //}
        //if ((parseInt(TotalUnits) + parseInt(TotalUnitsSplit)) != parseInt(HFTotalUnits)) {
        //    alert("TotalUnits not match with existing TotalUnits");
        //    return;
        //}
        //if ((parseFloat(Pointer) + parseFloat(PointerSplit)) != parseFloat(HFPointer)) {
        //    alert("Pointer not match with existing pointer");
        //    return;
        //}
        //if ((parseInt(PcsUnit) + parseInt(PcsUnitSplit)) != parseInt(HFPcsUnit)) {
        //    alert("PcsUnit not match with existing PcsUnit");
        //    return;
        //}

        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            url: SplitData,
            data: JSON.stringify({ HFOrderDetailsID: HFOrderDetailsID, MMSize: MMSize, TotalUnits: TotalUnits, Pointer: Pointer, PcsUnit: PcsUnit, MMSizeSplit: MMSizeSplit, TotalUnitsSplit: TotalUnitsSplit, PointerSplit: PointerSplit, PcsUnitSplit: PcsUnitSplit }),
            dataType: "json",
            success: function (data) {
                alert("Data Save Successfully");
                location.reload();
            },
            error: function (result) {
                alert("Error");
            }
        });
    });
}

function SaveCreateSODetails() {
    $('#BtnSaveCreateSO').click(function () {
        //;
        var HFOrderDetailsID = $('#HFOrderDetailsID').val();

        var HFPONo = $('#HFPONo').val();
        var HFSONo = $('#HFSONo').val();

        var SONo = $("#CreateSONo").val();

        var errormsg = "";
        if ($('#trCreateSOHeader input[type=radio]:checked').length <= 0) {
            errormsg = errormsg + "New/Existing Type is required. \n";
        }

        if (SONo == "") {
            errormsg = errormsg + "SONo is required.\n";
        }

        if (SONo == HFSONo) {
            errormsg = errormsg + "Cannot use same sales order number.\n";
        }

        if ($('#trCreateSOHeader input[type=radio]:checked').val() == 'New') {
            var AllSONo = $('#tblOrderList').jqGrid('getCol', 'SONo')

            for (var i = 0; i < AllSONo.length; i++) {
                var ExistingSONo = AllSONo[i];

                if (ExistingSONo == SONo) {
                    errormsg = errormsg + "SONo already exists.\n";
                }
            }
        }

        if (errormsg != "") {
            alert(errormsg);
            return false;
        }

        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            url: CreateSO,
            data: JSON.stringify({ HFOrderDetailsID: HFOrderDetailsID, HFPONo: HFPONo, SONo: SONo }),
            dataType: "json",
            success: function (data) {
                alert("Sales Order Created Successfully!!!");
                location.reload();
            },
            error: function (result) {
                alert("Error");
            }
        });
    });
}

function SOAutoComplete() {
    $("#CreateSONo").autocomplete({

        source: function (request, response) {
            var HFPONo = $('#HFPONo').val();

            if ($('#trCreateSOHeader input[type=radio]:checked').val() == 'Existing') {
                $.ajax({
                    url: GetSONo,
                    type: "POST",
                    datatype: "json",
                    data: { Prefix: request.term, HFPONo: HFPONo },
                    success: function (data) {
                        if (!data.length) {
                            var result = [
                             {
                                 label: 'No matches found',
                                 value: $("#CreateSONo").val('')
                             }
                            ];
                            response(result);
                        }
                        else {
                            response($.map(data, function (item) {

                                return { label: item.SONo, value: item.SONo };

                            }))
                        }
                    }
                })
            }
        },

    });
}

function SaveSplitNewDetails() {
    $('#BtnSaveSplitNew').click(function () {
        //;
        var errormsg = ""; var SplitPcsUnits = ""; var SplitSumPcsUnit = ""; var SplitItem = [];
        var HFOrderDetailsID = $('#HFOrderDetailsID').val();
        SplitPcsUnits = $('#HFSplitPcsUnit').val();
        //  SplitSumPcsUnit = $('#HFSplitPcsUnitSum').val();

        var ids = jQuery("#tblSplitNew").jqGrid('getDataIDs');

        for (var i = 0; i < ids.length; i++) {
            
            jQuery("#tblSplitNew").jqGrid('saveRow', ids[i]);
            jQuery("#tblSplitNew").jqGrid('editRow', ids[i], true);
        }
        var SplitGridData = $("#tblSplitNew").jqGrid("getGridParam", "data");
        //SplitItem += SplitGridData.PcsUnit;
        var Item =
            {
                lstSplitElementList: SplitGridData
            };
        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            url: SumColumnValues,
            dataType: "json",
            data: JSON.stringify(Item),
            async: false,
            success: function (data) {
                ;
                SumValueToSplit = data;
                $('#HFSplitPcsUnitSumNew').val(SumValueToSplit);
                //return data;
            },
            error: function (result) {
                alert("Error");
            }
        });
        ;
        var HFSplitSumOfPcs = $('#HFSplitPcsUnitSumNew').val();
        //GetCalculatedSum(Item);
        for (m = 0; m < SplitGridData.length; m++) {
            if (SplitGridData[m].PoDetails != "") {
                if (SplitGridData[m].PoDetails == 0) {
                    errormsg = errormsg + "Po Details can not hold value 0.\n";
                    //$('#lblErrMMSize0').show();
                    //errormsg = true;
                }
            }
            else {
                errormsg = errormsg + "Po Details can not be left blank.\n";
                //$('#lblErrMMSizeBlank').show();
                //errormsg = true;
            }

            if (SplitGridData[m].MMSize != "") {
                if (SplitGridData[m].MMSize == 0) {
                    errormsg = errormsg + "MM size can not hold value 0.\n";
                    //$('#lblErrMMSize0').show();
                    //errormsg = true;
                }
            }
            else {
                errormsg = errormsg + "MM size can not be left blank.\n";
                //$('#lblErrMMSizeBlank').show();
                //errormsg = true;
            }

            //if ($('#Pointer') != "") { 
            //    if ($('#Pointer') == 0) {

            //        errormsg = errormsg + "Pointer can not hold value 0.\n";
            //    }
            //}
            //else {

            //    errormsg = errormsg + "Pointer can not be left blank.\n";
            //}
            var item = {};
            item["Pointer"] = Pointer;
            var PointerValue = item["Pointer"];
            
            if (PointerValue!= "") {
                if (PointerValue == 0) {

                    errormsg = errormsg + "Pointer can not hold value 0.\n";
                }
            }
            else {

                errormsg = errormsg + "Pointer can not be left blank.\n";
            }
            if (SplitGridData[m].PcsUnit != "") {
                if (SplitGridData[m].PcsUnit == 0) {

                    errormsg = errormsg + "PcsUnit can not hold value 0.\n";
                }
                if (parseInt(SplitPcsUnits) != parseInt(HFSplitSumOfPcs)) {
                    //;
                    //$('#lblErrPCsMatch').show();
                    errormsg = errormsg + "PcsUnit not match with existing PcsUnit.\n";
                }
            }
            else {

                errormsg = errormsg + "PcsUnit can not be left blank.\n";
            }


            if (SplitGridData[m].TotalTolerance != "") {
                if (SplitGridData[m].TotalTolerance == 0) {
                    $('#lblErrTotalTolerance0').show();
                    errormsg = errormsg + "Total Tolerance can not hold value 0.\n";
                }
            }
            else {

                errormsg = errormsg + "Total Tolerance can not be left blank.\n";
            }

            if (SplitGridData[m].TotalUnits == "") {

                errormsg = errormsg + "Total units can not be left blank.\n";
            }
            if (SplitGridData[m].Material == "") {

                errormsg = errormsg + "Material can not be left blank.\n";
            }
            if (SplitGridData[m].MatDesc == "") {

                errormsg = errormsg + "Material description can not be left blank.\n";
            }
            if (errormsg != "") {
                alert(errormsg);
                return false;
            }
        }
        //
        //if (parseInt(SplitPcsUnits) != parseInt(HFSplitSumOfPcs)) {
        //    //;
        //    //$('#lblErrPCsMatch').show();
        //    errormsg = errormsg + "PcsUnit not match with existing PcsUnit.\n";
        //}
        //if (errormsg != "") {
        //    alert(errormsg);
        //    return false;
        //}

        //console.log(SplitGridData);

        var dataItem =
       {
           lstOrderList: SplitGridData, HFOrderDetailsID: HFOrderDetailsID
       };

        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            url: SplitData,
            data: JSON.stringify(dataItem),
            dataType: "json",
            success: function (data) {
                alert("Data Save Successfully");
                location.reload();
            },
            error: function (result) {
                alert("Error");
            }
        });

    });

}

function Validation() {
    $("#trCreateSOHeader input[type=radio]").click(function () {
        //alert('You clicked radio!');
        $("#CreateSONo").val('');
    });

    //Start Merging Validation

    $(document).on('blur', '#Pointer', function () {
        //;
        var Pointer = $(this).val();
        var PcsUnit = $("#PcsUnit").val();
        $('#CTSPerUnit').val((parseFloat(Pointer) * parseInt(PcsUnit)).toFixed(2));
        $('#lblErrPointer0Merge').hide();
        $('#lblErrPointerBlankMerge').hide();
    });

    $(document).on('blur', '#PcsUnit', function () {
        //;
        var PcsUnit = $(this).val();
        var Pointer = $("#Pointer").val();
        var TotalUnits = $("#HFTotalUnits").val();
        $('#CTSPerUnit').val((parseFloat(Pointer) * parseInt(PcsUnit)).toFixed(2));

        $('#TotalCTS').val((parseFloat($('#CTSPerUnit').val()) * parseInt(TotalUnits)).toFixed(2));
        //$('#lblErrPcs0Merge').hide();
        //$('#lblErrPcsBlankMerge').hide();
        //$('#lblErrPCsMatchMerge').hide();
    });

    $(document).on('blur', '#TotalUnits', function () {
        //;
        var TotalUnits = $(this).val();
        var CTSPerUnit = $("#CTSPerUnit").val();
        $('#TotalCTS').val((parseFloat(CTSPerUnit) * parseInt(TotalUnits)).toFixed(2));
        $('#lblErrTotalUnitsBlankMerge').hide();
    });

    //End Merging Validation

    //Start Split Validation for Existing Row
    $(document).on('blur', '#Pointer1', function () {
        //;
        var Pointer1 = $(this).val();
        var PcsUnit1 = $("#PcsUnit1").val();
        $('#CTSPerUnit1').val((parseFloat(Pointer1) * parseInt(PcsUnit1)).toFixed(2));
    });

    $(document).on('blur', '#PcsUnit1', function () {
        //;
        var PcsUnit1 = $(this).val();
        var Pointer1 = $("#Pointer1").val();
        $('#CTSPerUnit1').val((parseFloat(Pointer1) * parseInt(PcsUnit1)).toFixed(2));
    });

    $(document).on('blur', '#TotalUnits1', function () {
        //;
        var TotalUnits1 = $(this).val();
        var CTSPerUnit1 = $("#CTSPerUnit1").val();
        $('#TotalCTS1').val((parseFloat(CTSPerUnit1) * parseInt(TotalUnits1)).toFixed(2));
    });

    //End Split Validation for Existing Row

    //Start Split Validation for Split Row
    $(document).on('blur', '#Pointer2', function () {
        //;
        var Pointer2 = $(this).val();
        var PcsUnit2 = $("#PcsUnit2").val();
        $('#CTSPerUnit2').val((parseFloat(Pointer2) * parseInt(PcsUnit2)).toFixed(2));
    });

    $(document).on('blur', '#PcsUnit2', function () {
        //;
        var PcsUnit2 = $(this).val();
        var Pointer2 = $("#Pointer2").val();
        $('#CTSPerUnit2').val((parseFloat(Pointer2) * parseInt(PcsUnit2)).toFixed(2));
    });

    $(document).on('blur', '#TotalUnits2', function () {
        //;
        var TotalUnits2 = $(this).val();
        var CTSPerUnit2 = $("#CTSPerUnit2").val();
        $('#TotalCTS2').val((parseFloat(CTSPerUnit2) * parseInt(TotalUnits2)).toFixed(2));
    });

    //End Split Validation for Split Row
}

function ExportToExcel() {
    $("#BtnEtoE").click(function (e) {

        window.open('data:application/vnd.ms-excel,' + encodeURIComponent($('#dvData').html()));

        e.preventDefault();

    })
}



