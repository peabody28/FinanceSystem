
$("#loginForm").submit(function () {
    console.log("gere")
    $.ajax({
        url: "/Login/Login",
        method: "POST",
        data: $(this).serialize(),
        success: function (e)
        {
            $("#message").html("")
            if (e.errors != null)
                $.each(e.errors, function (key, value) {
                    $("#message").append(value + "<br/>")
                })
            else if (e.status == true)
                $(location).attr("href", "/")
            else
                $("#message").html("User isn't exist")
        }
    })
    return false;
})
