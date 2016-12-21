
/*
*验证手机号码
*/
function validatemobile(mobile) {
    if (mobile.length == 0) {
        showError("手机号码不能为空！");
        $("#mobile").focus();
        return false;
    }
    if (mobile.length != 11) {
        showError('请输入有效的手机号码！');
        $("#mobile").focus();
        return false;
    }

    var myreg = /^(((13[0-9]{1})|(15[0-9]{1})|(17[0-9]{1})|(18[0-9]{1}))+\d{8})$/;
    if (!myreg.test(mobile)) {
        showError('请输入有效的手机号码！');
        $("#mobile").focus();
        return false;
    }
    return true;
}

/*
*验证6位动态密码
*/
function validateDymaticCode(code) {
    var myreg = /^\d{6}$/;
    if (!myreg.test(code)) {
        showError('动态密码为6位数字！');
        $("#DynamicPWD").focus();
        return false;
    }
    return true;
}

/*
*显示错误信息 主要用于提示
*/
function showError(errorStr) {
    var container = $(document).find("[data-valmsg-summary=true]");
    list = container.find("ul");

    if (list && list.length) {
        list.empty();
        container.addClass("validation-summary-errors").removeClass("validation-summary-valid");
        $("<li />").html(errorStr).appendTo(list);
    }
}