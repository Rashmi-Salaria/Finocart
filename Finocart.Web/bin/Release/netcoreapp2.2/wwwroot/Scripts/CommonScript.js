 $(document).ready(function(){
     $('.navbar-toggle').on('click', function () {
        
         //$('.sidesmall').toggleClass('shownv');
         $('.footer-abt').toggleClass('hidefoot');
         $('.right-section-box').toggleClass('expand');
         if (window.innerWidth >= 1200) {
            // alert('bigger');
             $('.sidebig').toggleClass('hidenv');
         }
         else {
             //alert('smaller');
             $('.sidebig').toggleClass('mobAside');
         }
     });
     var alterClass = function () {
         var ww = document.body.clientWidth;
         if (ww < 1200) {
             $('.sidebar-section').removeClass('openSidebar');
         } else if (ww >= 1200) {
             $('.sidebar-section').addClass('openSidebar');
         };
     };
     $(window).resize(function () {
         alterClass();
         if (window.innerWidth >= 1200) {
             // alert('bigger');
             $('.sidebig').toggleClass('shownv');
         }
         else {
             //alert('smaller');
             $('.sidebig').toggleClass('mobAside');
         }
     });
     //Fire it when the page first loads:
     alterClass();

     var path = window.location.href; // because the 'href' property of the DOM element is the absolute path
     $('#myNavbar ul li').removeClass('active');
     $('#myNavbar ul li a').each(function () {
         if (this.href === path) {
             $(this).parents('li').addClass('active');
         }
     });

     //show password
     
     $('#showPassword').on('click', function () {
         console.log('ok');

         if ($('#txtPassword').attr('show_pass') == 'false') {

             $('#txtPassword').removeAttr('type');
             $('#txtPassword').attr('type', 'text');

             $('#txtPassword').removeAttr('show_pass');
             $('#txtPassword').attr('show_pass', 'true');

             $('#showPassword i').removeClass('fa-eye-slash').addClass('fa-eye');

         } else {

             $('#txtPassword').removeAttr('type');
             $('#txtPassword').attr('type', 'password');

             $('#txtPassword').removeAttr('show_pass');
             $('#txtPassword').attr('show_pass', 'false');

             $('#showPassword i').removeClass('fa-eye').addClass('fa-eye-slash');

         }

     });
     //show password end
 });


