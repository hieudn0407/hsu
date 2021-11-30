app.controller('AccountController', ['$scope', '$timeout', '$http', function ($scope, $timeout, $http) {

    $scope.SendNotification = () => {
        connectionAccount.invoke("SendNotification", $scope.NotificationTextSend).catch(function (err) {
            return console.error(err.toString());
        });
    };

    $scope.goLogin = () => {
        var username = $scope.Username;
        var password = $scope.Password;

        connectionAccount.invoke("Login", username, password).catch(function (err) {
            return console.error(err.toString());
        });
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
}]);