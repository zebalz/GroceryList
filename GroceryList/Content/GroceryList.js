(function () {
    angular.module("GroceryList", [])
})();




//AUTOFOCUS DIRECTIVE - (update items)
(function () {
    angular.module("GroceryList")
        .directive('autofocus', ['$timeout', function ($timeout) {
            return {
                restrict: 'A',
                link: function ($scope, $element) {
                    $timeout(function () {
                        $element[0].focus();
                    });
                }
            }
        }]);
})();

(function () {
    angular.module("GroceryList")
        .controller("rowController", rowController)

    rowController.$inject = ["glService"];

    function rowController(glService) {

        var rc = this;
        rc.updateI = _updateItemOrQuantity;

        function _updateItemOrQuantity(item) {
            console.log(item);
            glService.update(item).then(_updateItemOrQuantitySuccess, _updateItemOrQuantityFail);
        }

        function _updateItemOrQuantitySuccess(response) {
            console.log(response);
        }

        function _updateItemOrQuantityFail(error) {
            alert(error);
        }
    }
})();


(function () {
    angular.module("GroceryList")
        .controller("groceryController", groceryController)

    groceryController.$inject = ["glService"];

    function groceryController(glService) {

        var glcc = this;
        glcc.$onInit = _getGroceryItems;
        glcc.post = _postNewItem;
        glcc.itemList = null;
        glcc.data = {};
        glcc.select = _select;
        glcc.deleteItem = _deleteOneItem;
        glcc.clearAll = _clearList;

        function _select(item, index) { //index is the position of the selected item
            glcc.data.Id = item.Id;
            console.log(glcc.data.Id);
            glcc.index = index;
        }
        function _getGroceryItems() {
            glService.get().then(_getGroceryItemsSuccess, _getGroceryItemsFail);
        }
        function _getGroceryItemsSuccess(response) {
            glcc.itemList = response.data;
            console.log(response.data);
        }
        function _getGroceryItemsFail(error) {
            console.log(error);
        }
        function _postNewItem(data) { 
            glService.post(glcc.data).then(_postNewItemSuccess, _postNewItemFail);
        }
        function _postNewItemSuccess(response) {
            glcc.data = null;
            glService.get().then(_getGroceryItemsSuccess, _getGroceryItemsFail);
            console.log(response);
        }
        function _postNewItemFail(error) {
            alert(error);
        }
        function _deleteOneItem(id, index) {
            glcc.index = index;
            console.log(index);
            glService.deleteSingle(id).then(_deleteOneItemSuccess, _deleteOneItemFail);
            glcc.itemList.splice(glcc.index, 1);
        }
        function _deleteOneItemSuccess(response) {
            console.log(response);
        }
        function _deleteOneItemFail(error) {
            alert(error);
        }

        function _clearList() {
            glService.deleteEverything().then(_clearListSuccess, _clearListFail);
        }
        function _clearListSuccess(response) {
            glService.get().then(_getGroceryItemsSuccess, _getGroceryItemsFail);
        }
        function _clearListFail(error) {
            alert(error);
        }
    }
})();

(function () {
    angular.module("GroceryList")
        .service("glService", glService)

    glService.$inject = ["$http"];

    function glService($http) {

        this.get = _getList;
        this.post = _insertIntoList;
        this.deleteSingle = _deleteItem4rmList;
        this.deleteEverything = _deleteAll;
        this.update = _updateItem;

        //GET LIST OF GROCERIES
        function _getList() {
            var settings = {
                url: "/api/GroceryList/getItems"
                , method: "GET"
                , cache: false //or else browsers refresh and you lose data
                , contentType: "application/json"
            };
            return $http(settings)
                .then(_getListSuccess, _getListFail);
        } 
        function _getListSuccess(response) {
            return response;
        }
        function _getListFail(error) {
            return error;
        }
        //INSERT NEW ITEM
        function _insertIntoList(data){
            var settings = {
                url: "/api/GroceryList/newItem"
                , method: "POST"
                , data: data
                , cache: false
                , contentType: "application/json"
            };
            return $http(settings)
                .then(_insertIntoListSuccess, _insertIntoListFail);
        }
        function _insertIntoListSuccess(response) {
            return response;
        }
        function _insertIntoListFail(error) {
            return error;
        }
        //UPDATE ITEM
        function _updateItem(data) {
            var settings = {
                url: "/api/GroceryList/" + encodeURIComponent(data.Id)
                , method: "PUT"
                , cache: false
                , data: data
            };
            return $http(settings)
                .then(_updateItemSuccess, _updateItemFail);
        }
        function _updateItemSuccess(response) {
            return response;
        }
        function _updateItemFail(error) {
            return error;
        }
        //DELETE SOLO ITEM
        function _deleteItem4rmList(id) {
            var settings = {
                url: "/api/GroceryList/" + encodeURIComponent(id)
                , method: "DELETE"
                , cache: false
                , data: id
            };
            return $http(settings)
                .then(_deleteItem4rmListSuccess, _deleteItem4rmListFail);
        }
        function _deleteItem4rmListSuccess(response) {
            return response;
        }
        function _deleteItem4rmListFail(error) {
            return error;
        }
        //DELETE ALL ITEMS
        function _deleteAll() {
            var settings = {
                url: "/api/GroceryList/DeleteAll"
                , method: "DELETE"
                , cache: false
            }
            return $http(settings)
                .then(_deleteAllSuccess, _deleteAllFail);
        }
        function _deleteAllSuccess(response) {
            return response;
        }
        function _deleteAllFail(error) {
            return error;
        }
    }
})();