//var Popup, dataTable;
//$(document).ready(function () {
//    dataTable = $("#sinhvienTable").DataTable({
//        "processing": true,
//        "serverSide": true,
//        "ajax": {
//            'url': '/Khoa/GetList',
//            'type': 'POST',
//            'dataType': "json",
//            "dataSrc": "data"
//        },
//        "columns": [
//            { "data": "maKhoa" },
//            { "data": "tenKhoa" },
//            {
//                "render": function (data) {
//                    console.log(data);
//                    /*return "<a class='btn btn-default btn-sm' onclick=PopupForm('@Url.Action("Edit", "Khoa")/" + data + "')> <i class='fa fa-pencil'></i> Edit</a><a class='btn btn-danger btn-sm' style='margin-left:5px' onclick=Delete(" + data + ")><i class='fa fa-trash'></i> Delete</a>";*/
//                },
//                "orderable": false,
//                "searchable": false,
//                "width": "150px"
//            }
//        ],
//        "language": {

//            "emptyTable": "No data found, Please click on <b>Add New</b> Button"
//        }
//    });
//    var table = $('#sinhvienTable').DataTable();
//    $('#sinhvienTable tbody').on('click', 'tr', function () {
//        $(this).toggleClass('selected');
//    });
//    $('#button').click(function () {
//        alert(table.rows('.selected').data().length + ' row(s) selected');
//    });
//});