app.controller('NewsController', ['$scope', '$timeout', '$http', function ($scope, $timeout, $http) {

    if (connectionNews.q == "Disconnected") {
        connectionNews.start().then(function () {
        }).catch(function (err) {
            return console.error(err.toString());
        });
    }

    $scope.upsert = false;
    $scope.mainform = true;
    $scope.NewsObject = {
        id: 0,
        name: "",
        image: "",
        isactived: false,
        is_show_home: false,
        html: ""
    };

    $scope.AddNews = () => {
        $("#editorDetail").data("kendoEditor").value("");
        $scope.NewsObject = {
            id: 0,
            name: "",
            image: "",
            isactived: false,
            is_show_home: false,
            html: ""
        };

        $scope.upsert = true;
        $scope.mainform = false;
    };

    $scope.SaveNews = () => {
        $scope.NewsObject.html = $("#editorDetail").data("kendoEditor").value();
        connectionNews.invoke("AddNews", global_token, $scope.NewsObject).catch(function (err) {
            return console.error(err.toString());
        });
    };

    $scope.Back = () => {
        $scope.Find();

        $scope.upsert = false;
        $scope.mainform = true;
    };

    $scope.Find = () => {
        connectionNews.invoke("ListNews", global_token, 1).catch(function (err) {
            return console.error(err.toString());
        });
    };

    $scope.EditNews = (news) => {
        $("#editorDetail").data("kendoEditor").value(news.html);
        $scope.NewsObject = news;
        $scope.upsert = true;
        $scope.mainform = false;
    };

    connectionNews.on("ReceiveAddNews", function (rs, cookie_token) {
        if (rs.code != 200) {
            alert(rs.message);
            return;
        }

        update_token(cookie_token);
        alert("Lưu Thành Công");
        $scope.Back();
        $scope.$apply();
    });

    connectionNews.on("ReceiveListNews", function (rs, cookie_token) {
        if (rs.code != 200) {
            alert(rs.message);
            return;
        }

        $scope.listnews = rs.data;
        update_token(cookie_token);
        $scope.$apply();
    });

    $("#editorDetail").kendoEditor({
        "resizable": { "content": true },
        "tools": [
            "bold", "italic", "underline", "strikethrough", "justifyLeft", "justifyCenter", "justifyRight", "justifyFull", "insertUnorderedList", "insertOrderedList", "indent",
            "outdent", "createLink", "unlink", "insertImage", "insertFile", "subscript", "superscript", "tableWizard", "createTable", "addRowAbove", "addRowBelow", "addColumnLeft",
            "addColumnRight", "deleteRow", "deleteColumn", "viewHtml", "formatting", "cleanFormatting", "fontName", "fontSize", "foreColor", "backColor", "print"
        ],
        "imageBrowser": {
            "transport": {
                "read": { "url": "/get-director" },
                "type": "imagebrowser-aspnetmvc",
                "thumbnailUrl": "/get-image",
                "uploadUrl": "/upload-file",
                "destroy": { "url": "/delete" },
                "create": { "url": "/create-folder" },
                "imageUrl": "https://localhost:44364/image/{0}"
            }
        }
    });
}]);