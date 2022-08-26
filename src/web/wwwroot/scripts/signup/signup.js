
$("#signUpForm").submit(function () {
    $.ajax({
        url: "/SignUp/Create",
        method: "POST",
        data: $(this).serialize(),
        success: function (e)
        {
            $("#message").html("")
            if (e.errors != null)
                $.each(e.errors, function (key, value) {
                    $("#message").append(value + "<br/>")
                })
            else
                $(location).attr("href", e.url)     
        }
    })
    return false;
})
