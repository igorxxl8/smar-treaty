(() => {
    $(document).ready(() => {
        $("#dismiss, .overlay").on("click",
            function() {
                $("#sidebar").removeClass("active");
                $(".overlay").removeClass("active");
            });

        $("#sidebarCollapse").on("click",
            function() {
                $("#sidebar").addClass("active");
                $(".overlay").addClass("active");
                $(".collapse.in").toggleClass("in");
            });

        $("#upBut").click(() => {
            $("html, body").animate({
                    scrollTop: 0
                },
                1000);
        });

        $("#closeSidebar").on("click",
            () => {
                $("#dismiss, .overlay").trigger("click");
            });
    });
})();