var taskManagementUrl = "http://localhost:61589/api/V1";

var indexViewModel = function() {

    var self = this;

    self.statusMessage = ko.observable("(Click Refresh button to load tasks)");

    self.refreshTasks = function() {
        var token = $.cookie('UserToken');

        $.ajax({
            type: 'GET',
            url: taskManagementUrl + '/tasks',
            headers: {
                Authorization : "Bearer " + token
            },
            contentType: 'application/json;charset=utf8',
            success: self.onRefreshSuccess,
            error: self.onRefreshError
        });
    };

    self.onRefreshSuccess = function(data, status) {
        self.statusMessage(JSON.stringify(data, null, 4));
    };

    self.onRefreshError = function(error) {
        self.statusMessage(error.responseText);
    };
};
