

$(document).ready(function () {
    
    jQuery("#tblDemoList").jqGrid({
        
        url: GetDemoDetail,
        datatype: "json",
        rowNum: 10,
        height: 'auto',
        width: 900,
        //rowList: [10, 20, 30],  delRowData
        sortname: 'CategoryName',
        shrinkToFit: true,
        sortorder: "desc",
        colNames: ['Action','ID', 'CategoryName', 'MaxValue'],
        colModel: [
                    { name: 'act', index: 'act', width: 75, sortable: false },
                    { name: 'ID', index: 'ID', sortable: true, hidden: true, title: false},
                    { name: 'CategoryName', index: 'CategoryName', sortable: true, editable: true },
                    { name: 'MaxValue', index: 'MaxValue', sortable: true, editable: true }


        ],
        pager: '#pager',
        pgbuttons: true,
        viewrecords: true,       
        caption: "Demo List",
        
        gridComplete: function () {
            
            var ids = jQuery("#tblDemoList").jqGrid('getDataIDs');
            for (var i = 0; i < ids.length; i++) {
                var cl = ids[i];                
                be = "<input style='height:22px;width:20px;' type='button' value='E' onclick=\"jQuery('#tblDemoList').editRow('" + cl + "');\"  />";
                se = "<input style='height:22px;width:20px;' type='button' value='S' onclick=\"jQuery('#tblDemoList').saveRow('" + cl + "');\"  />";
                ce = "<input style='height:22px;width:20px;' type='button' value='C' onclick=\"jQuery('#tblDemoList').restoreRow('" + cl + "');\" />";
                //de = "<input style='height:22px;width:20px;' type='button' value='D' onclick=\"jQuery('#tblDemoList').delGridRow('" + cl + "');\" />";
                jQuery("#tblDemoList").jqGrid('setRowData', ids[i], { act: be + se + ce});                
            }

        },
        editurl: SaveData,        
        subGrid: true,
        subGridOptions: {
            plusicon: "ui-icon-plus",
            minusicon: "ui-icon-minus",
            openicon: "ui-icon-carat-1-sw",
            //expandOnLoad: true,
            selectOnExpand: false,
            reloadOnExpand: false
        },
        subGridRowExpanded: function (subgrid_id, row_id) {
            var rowData = $(this).getRowData(row_id);
            //var selectedVault = rowData.VaultID;
            //var maxVault = rowData.MaximumValue.replace(/\,/g, '');
            var subgrid_table_id;
            subgrid_table_id = subgrid_id + "_t";
            var pagerId = "jqGridPager_" + subgrid_id;
            jQuery("#" + subgrid_id).html("<table id='" + subgrid_table_id + "' class='scroll'></table><div id='" + pagerId + "'></div>");
            jQuery("#" + subgrid_table_id).jqGrid({
                url: GetDetail,
                datatype: "json",
                rowNum: 10,
                height: 'auto',
                width: 900,
                //rowList: [10, 20, 30],     , formatter: FirstNameFormatter , formatter: LastNameFormatter  , formatter: SalaryFormatter
                sortname: 'CategoryName',
                shrinkToFit: true,
                sortorder: "desc",
                colNames: ['Action','ID', 'FirstName', 'LastName', 'Salary'],
                colModel: [
                            { name: 'act', index: 'act', width: 75, sortable: false },
                            { name: 'ID', index: 'ID', sortable: true, hidden: true, title: false },
                            { name: 'FirstName', index: 'FirstName', width: '100', sortable: false, editable: true },
                            { name: 'LastName', index: 'LastName', sortable: false, editable: true },
                            { name: 'Salary', index: 'Salary', sortable: false, editable: true }


                ],
                pager: '#pager',
                pgbuttons: true,
                viewrecords: true,

                caption: "Demo Details List",

                gridComplete: function () {
                    
                    var ids1 = jQuery("#" + subgrid_table_id).jqGrid('getDataIDs');
                    var SubGridID = jQuery("#" + subgrid_table_id);
                    for (var i = 0; i < ids1.length; i++) {
                        var cl = ids1[i];
                        be = "<input style='height:22px;width:20px;' type='button' value='E' onclick=\"jQuery('#" + subgrid_table_id + "').editRow('" + cl + "');\"  />";
                        se = "<input style='height:22px;width:20px;' type='button' value='S' onclick=\"jQuery('#" + subgrid_table_id + "').saveRow('" + cl + "');\"  />";
                        ce = "<input style='height:22px;width:20px;' type='button' value='C' onclick=\"jQuery('#" + subgrid_table_id + "').restoreRow('" + cl + "');\" />";
                        jQuery("#" + subgrid_table_id).jqGrid('setRowData', ids1[i], { act: be + se + ce });
                    }
                   
                },
                editurl: SaveDetailData,
                //subGrid: true,
                //subGridRowExpanded: function (subgrid_id, row_id) {
                //    var pagerId = "jqGridPager_" + subgrid_id;
                //    var subgrid_table_id;
                //    subgrid_table_id = subgrid_id + "_t";
                //    jQuery("#" + subgrid_id).html("<table id='" + subgrid_table_id + "' class='scroll'></table><div id='" + pagerId + "'></div>");
                //    jQuery("#" + subgrid_table_id).jqGrid({
                //        url: GetDemoDetail,
                //        datatype: "json",
                //        rowNum: 10,
                //        height: 'auto',
                //        width: 900,
                //        //rowList: [10, 20, 30],
                //        sortname: 'CategoryName',
                //        shrinkToFit: true,
                //        sortorder: "desc",
                //        colNames: ['ID', 'CategoryName', 'MaxValue'],
                //        colModel: [
                //                    { name: 'ID', index: 'ID', sortable: true, hidden: true, title: false },
                //                    { name: 'CategoryName', index: 'CategoryName', sortable: true },
                //                    { name: 'MaxValue', index: 'MaxValue', sortable: true }


                //        ],
                //        pager: '#pager',
                //        pgbuttons: true,
                //        viewrecords: true,

                //        caption: "Demo List",

                //        subGrid: true,
                //        subGridRowExpanded: function (subgrid_id, row_id) {
                //            var pagerId = "jqGridPager_" + subgrid_id;
                //            var subgrid_table_id;
                //            subgrid_table_id = subgrid_id + "_t";
                //            jQuery("#" + subgrid_id).html("<table id='" + subgrid_table_id + "' class='scroll'></table><div id='" + pagerId + "'></div>");
                //            jQuery("#" + subgrid_table_id).jqGrid({
                //                url: GetDemoDetail,
                //                datatype: "json",
                //                rowNum: 10,
                //                height: 'auto',
                //                width: 900,
                //                //rowList: [10, 20, 30],
                //                sortname: 'CategoryName',
                //                shrinkToFit: true,
                //                sortorder: "desc",
                //                colNames: ['ID', 'CategoryName', 'MaxValue'],
                //                colModel: [
                //                            { name: 'ID', index: 'ID', sortable: true, hidden: true, title: false },
                //                            { name: 'CategoryName', index: 'CategoryName', sortable: true },
                //                            { name: 'MaxValue', index: 'MaxValue', sortable: true }


                //                ],
                //                pager: '#pager',
                //                pgbuttons: true,
                //                viewrecords: true,

                //                caption: "Demo List",
                //            });
                //        }

                //    });
                //}
            });
        }
        
    });

  
    //jQuery("#tblDemoList").jqGrid('filterToolbar', { searchOperators: true });

    //function CategoryNameFormatter(cellvalue, options, rowObject) {
    //    if (rowObject.CategoryName == null)
    //        rowObject.CategoryName = "";
    //    return '<input type="text" class="form-control" id="txtCategoryName' + options.rowId + '" value="' + rowObject.CategoryName + '" name="CategoryName" value="" onkeypress="" />';
    //}

    function FirstNameFormatter(cellvalue, options, rowObject) {
        if (rowObject.FirstName == null)
            rowObject.FirstName = "";
        return '<input type="text" class="form-control" id="txtFirstName' + options.rowId + '" value="' + rowObject.FirstName + '" name="FirstName" value="" onkeypress="" />';
    }

    function LastNameFormatter(cellvalue, options, rowObject) {
        if (rowObject.LastName == null)
            rowObject.LastName = "";
        return '<input type="text" class="form-control" id="txtLastName' + options.rowId + '" value="' + rowObject.LastName + '" name="LastName" value="" onkeypress="" />';
    }

    function SalaryFormatter(cellvalue, options, rowObject) {
        if (rowObject.Salary == null)
            rowObject.Salary = "";
        return '<input type="text" class="form-control" id="txtSalary' + options.rowId + '" value="' + rowObject.Salary + '" name="Salary" value="" onkeypress="numericFilter(this)" />';
    }


    //$("#BtnSave").click(function (e) {
    //    
    //    var rids = $('#tblDemoList').jqGrid('getDataIDs');
    //    var subgridNameArray = [];
       
    //    for (var i = 0; i < rids.length; i++) {
    //        var nth_row_id = rids[i];
    //        var subgridName = "#tblDemoList" + nth_row_id + "_t";
    //        subgridNameArray[i] = subgridName;
    //        alert(subgridNameArray[i]);
    //    }


    //});

})

