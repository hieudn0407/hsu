app.controller('HeaderController', ['$scope', '$timeout', '$http', function ($scope, $timeout, $http) {
    $scope.LoginModal = false;
    $scope.RegisterModal = false;
    $scope.Notification = false;

    if (connectionAccount.q == "Disconnected") {
        connectionAccount.start().then(function () {
        }).catch(function (err) {
            return console.error(err.toString());
        });
    }

    $scope.LoadMenu = () => {
        var menu = document.getElementsByClassName("menu-overload");
        menu[0].removeAttribute("hidden");

        setTimeout(function () {
            var menubody = document.getElementsByClassName("menu-body");
            menubody[0].style.width = "270px";
            menubody[0].style.left = "0px";
        }, 100);
    };

    $scope.Login = () => {
        $scope.LoginModal = true;
        $scope.RegisterModal = false;
    };

    $scope.CloseLogin = () => {
        $scope.LoginModal = false;
    };

    $scope.goLogin = () => {
        var username = $scope.Username;
        var password = $scope.Password;

        connectionAccount.invoke("Login", username, password).catch(function (err) {
            return console.error(err.toString());
        });
    };

    $scope.Register = () => {
        $scope.RegisterModal = true;
        $scope.LoginModal = false;
    };

    $scope.CloseRegister = () => {
        $scope.RegisterModal = false;
    };

    $scope.goRegister = () => {
        var temp = true;
        var error = "";

        var fullname = $scope.RGFullname;
        var username = $scope.RGUsername;
        var phone = $scope.RGPhone;
        var password = $scope.RGPassword;
        var repassword = $scope.RGRePassword;

        if (!fullname || !username || !phone || !password || !repassword) {
            temp = false;
            error += "- Vui lòng điền đầy đủ thông tin.";
            alert(error);
            return;
        }

        if (!validation_phone(phone)) {
            temp = false;
            error += "- Định dạng sđt không đúng.";
            alert(error);
            return;
        }

        if (password != repassword) {
            temp = false;
            error += "- Xác nhận mật khẩu sai.";
            alert(error);
            return;
        }

        if (!temp) {
            alert(error);
            return;
        }

        connectionAccount.invoke("Register", fullname, phone, username, password).catch(function (err) {
            return console.error(err.toString());
        });
    };

    $scope.goKeno = () => {
        connectionAccount.invoke("LoginStatus", global_token, "/doan-so-chan-le").catch(function (err) {
            return console.error(err.toString());
        });
    };

    $scope.goNohu = () => {
        connectionAccount.invoke("LoginStatus", global_token, "/no-hu").catch(function (err) {
            return console.error(err.toString());
        });
    };

    connectionAccount.on("ReceiveAccount", function (response, cookie_token) {
        if (response.code == 200) {
            update_token(cookie_token);
            window.location.href = "/";
            return;
        }
        else {
            alert("tài khoản hoặc mật khẩu không đúng.");
        }
    });

    connectionAccount.on("CheckLoginStatus", function (response, cookie_token, url) {
        if (response.code == 200) {
            update_token(cookie_token);
            window.location.href = url;
            return;
        }
        else if (response.code == 201) {
            remove_cookie();
            window.location.href = "/";
        }
        else {
            remove_cookie();
            $scope.LoginModal = true;
            $scope.RegisterModal = false;
            $scope.$apply();
        }
    });

    connectionAccount.on("ReceiveRegister", function (response, cookie_token) {
        if (response.code == 200) {
            update_token(cookie_token);
            window.location.href = "/";
            return;
        }
        else {
            alert(response.message);
        }
    });
}]);