var Common = {
   
    SuccessNotify: function (e) {
        $.notify({
            title: "Sucess:",
            message: "New User Added Successfully."
            //Email Notification have been sent with Company ID and Registration Llink" +
            //    "Using which User will able to Register with Portal and Set Password for Future Portal Usage"
        }, {
                type: 'success',
                delay: 5000,
                background:'red',
                placement: {
                    from: "top",
                    align: "center",
                    
                }

            },
            

        );
    },

    UpdateNotify: function (e) {
        $.notify({
            title: "Sucess:",
            message: "User record updated successfully"
        }, {
                type: 'success',
                delay: 5000,
                placement: {
                    from: "top",
                    align: "center"
                }

            });
    },

    DeleteNotify: function (e) {
        $.notify({
            title: "Sucess:",
            message: "User record deleted successfully"
        }, {
                type: 'success',
                delay: 5000,
                placement: {
                    from: "top",
                    align: "center"
                }

            });
    },
    //if(confirm("Are you sure you want to delete this User page?")) {
    CustomSuccessNotify: function (Message) {
        $.notify({
            title: "Success:",
            message: Message
        }, {
                type: 'success',
                delay: 5000,
                placement: {
                    from: "bottom",
                    align: "right"
                }

            });
    },
      //  }

     CustomErrorNotify: function (Message) {
        $.notify({
            title: "Error:",
            message: Message
        }, {
                type: 'warning',
                delay: 5000,
                placement: {
                    from: "bottom",
                    align: "right"
                }

            });
    },
    ClearAllControls: function resetFields(form) {
        $(':input', form).each(function () {
            var type = this.type;
            var tag = this.tagName.toLowerCase(); // normalize case
           
            if (type == 'text' || type == 'password' || tag == 'textarea' || type == 'file')
                this.value = "";
            
            else if (type == 'checkbox' || type == 'radio')
                this.checked = false;
            
            else if (tag == 'select')
                this.selectedIndex = 0;
        });
    },

   DiffrenceBetweenDays: function showDays(firstDate, secondDate){//pass date in mm/dd/yyyy format
    var startDay = new Date(firstDate);
    var endDay = new Date(secondDate);
    var millisecondsPerDay = 1000 * 60 * 60 * 24;

    var millisBetween = startDay.getTime() - endDay.getTime();
    var days = millisBetween / millisecondsPerDay;
       return Math.floor(days);
    // Round down.
    

    },
    CustomMessageShow: function (Message) {
        $.notify({
            title: "Sucess:",
            message: Message 
        }, {
                type: 'success',
                delay: 5000,
                placement: {
                    from: "bottom",
                    align: "right"
                }

            });
    },

}
