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
        "users": [
            "6e0ee133-012c-4800-b7d2-68ea93ef15e1",
            "6e0ee133-012c-4800-b7d2-68ea93ef15e5",
            "6e0ee133-012c-4800-b7d2-68ea93ef15e4",
            "6e0ee133-012c-4800-b7d2-68ea93ef15e3",
            "6e0ee133-012c-4800-b7d2-68ea93ef15e2"
        ],
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
        users: [
            "6e0ee133-012c-4800-b7d2-68ea93ef15e1",
            "6e0ee133-012c-4800-b7d2-68ea93ef15e5",
            "6e0ee133-012c-4800-b7d2-68ea93ef15e4",
            "6e0ee133-012c-4800-b7d2-68ea93ef15e3",
            "6e0ee133-012c-4800-b7d2-68ea93ef15e2"
        ],
        inonuri: "uritoicon"
    });
    http.post('/api/roomapi/changeroom', requestData).then(function mySuccess(response) {
        console.log('success')
        console.log(response.data);
        document.location.href = "/";
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
    scope = $scope;
    http = $http;
});

//<script>
//    $(document).ready(function (){
//        $(".removeRoom").click(function () {
//            var roomId = "@Model.RoomID";
//            $.ajax({
//                type: 'POST',
//                url: '/api/roomapi/removeroom',
//                data: '{"roomid":"'+roomId+'"}', // or JSON.stringify ({name: 'jonas'}),
//                success: function (data) { alert('data: '); console.log('success'); document.location.href = "/"; },
//                contentType: "application/json",
//                dataType: 'json'
//            });
//        });
//    });
//</script>