
$("#createFinanceOperationForm").submit(function () {
    console.log("gere")
    $.ajax({
        url: "/FinanceOperation/Create",
        method: "POST",
        data: $(this).serialize(),
        success: function (e) {
            $("#message").html("")
            if (e.errors == null)
                $("#message").html("<div style='color:green'>Success</div><br/>ChainId: " + e.chainFk)
            else
                $.each(e.errors, function (key, value) {
                    $("#message").append(value+"<br/>")
                })
        }
    })
    return false;
})
