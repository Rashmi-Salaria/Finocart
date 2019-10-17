$(document).ready(function () {
    GetNotificationList();
});

function GetNotificationList() {
    $.ajax({
        type: "get",
        url: Notifications,
        contentType: 'application/json; charset=utf-8',
        success: function (result) {
            if (result.data2 > 0) {
                $("#notificationCount").show();
                $("#notificationCount").text(result.data2);
                //$("#dvNotification ul li:nth-child(2)").find("a:first span").show()
                //$("#dvNotification ul li:nth-child(2)").find("a:first span").text(result.data2)
            }
            var ul = $("<ul class='notification navigation1 dropdown-menu-right dropdown-menu'><li class='clearfix'><a href='#' class='clearfix'><h3>Notifications</h3></a></li></ul>")
            if (result.data != null) {
                $.each(result.data, function (i) {
                    var li = $("<li class='clearfix'><a href='#' class='clearfix'><p>" + result.data[i].notifications + "</p></a></li>")
                    li.appendTo(ul);
                });
            }
            $("<li class='clearfix'><a href='../Notification/NotificationList' class='clearfix'><h4>More Notifications</h4></a></li>").appendTo(ul);

            $("#dvNotification ul li:last").append(ul);
        },
        error: function () {
            alert("Error occured!!")
        }
    });
}
