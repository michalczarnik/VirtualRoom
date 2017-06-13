window.RoomRequestor = {
    addRoom: addRoom
}
var scope, http;
function addRoom(name, description, capacity) {
    console.log('adding room ' + name + ' ' + description + ' ' + capacity);
    var requestData = {
        "adminid": "6e0ee133-012c-4800-b7d2-68ea93ef15e0",
        "name": name,
        "description": description,
        "capacity": capacity,
        "users": [],
        "inonuri": "uritoicon"
    };
    http.post('/api/roomapi/addroom', requestData).then(function mySuccess(response) {
        console.log('success')
        console.log(response.data);
    }).catch(function myError(response) {
        console.log('error')
        console.log(response);
    });
};

function removeRoom(roomID) {
    console.log('removeRoom  ' + roomID);
    var requestData = JSON.stringify({roomid: roomID});
    http.post('/api/roomapi/removeroom', requestData).then(function mySuccess(response) {
        console.log('success')
        console.log(response.data);
        document.location.href = "/";
    }).catch(function myError(response) {
        console.log('error')
        console.log(response);
    });
}

function changeRoomInfo(roomID, name, description, capacity) {
    console.log('changeRoomInfo  ' + roomID);
    console.log(scope.change);

    var requestData = JSON.stringify({
        roomid: roomID,
        name: name,
        description: description,
        capacity: capacity,
        users: [],
        inonuri: "uritoicon"
    });
    http.post('/api/roomapi/changeroom', requestData).then(function mySuccess(response) {
        console.log('success');
        console.log(response.data);
        document.location.href = "/";
    }).catch(function myError(response) {
        console.log('error')
        console.log(response);
    });
}

function getUsers(roomID) {
    console.log('getUsers  ' + roomID);
    var requestData = JSON.stringify({ roomid: roomID });
    http.post('/api/roomapi/getusers', requestData).then(function mySuccess(response) {
        console.log('success')
        console.log(response.data);
        console.log(JSON.parse(response.data));
        JSON.parse(response.data).forEach(function(data) {
            $("#circleContainer").append("<div class='field'>" + data.Name + "</div>");
            console.log(data.Name);
        });
        var radius = 200;
        var fields = $('.field'), container = $('#container'), width = container.width(), height = container.height();
        var angle = 0, step = (2 * Math.PI) / fields.length;
        fields.each(function () {
            var x = Math.round(width / 2 + radius * Math.cos(angle) - $(this).width() / 2);
            var y = Math.round(height / 2 + radius * Math.sin(angle) - $(this).height() / 2);
            if (window.console) {
                console.log($(this).text(), x, y);
            }
            $(this).css({
                left: (x) + 'px',
                top: (y+250) + 'px'
            });
            angle += step;
        });
    }).catch(function myError(response) {
        console.log('error')
        console.log(response);
    });
}

var app = angular.module('VirtualRoom', []);
app.controller('VirtualRoomController', function ($scope, $http) {
    console.log('Controller intialized')
    $scope.addRoom = addRoom;
    $scope.removeRoom = removeRoom;
    $scope.changeRoomInfo = changeRoomInfo;
    $scope.getUsers = getUsers;
    scope = $scope;
    http = $http;
});
