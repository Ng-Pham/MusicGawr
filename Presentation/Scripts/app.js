var app = angular.module('MyApp', []);
app.controller('MyController', function ($scope, $http) {
    $http.get('data/user.json').then(function (res) {
        $scope.user = res.data;
    });
    
    $scope.accinfo = null;
    var accStorage = sessionStorage.getItem('_login');
    if (accStorage) {
        $scope.accinfo = angular.fromJson(accStorage);
    }
    $scope.login = function () {
        var std = findacc($scope.email, $scope.password);
        if (std) {
            $scope.accinfo = std;
            $scope.mess = '';
            sessionStorage.setItem('_login', angular.toJson(std));
        } else {
            $scope.mess = 'Không tìm thấy thông tin tài khoản';
        }
    }

    function findacc(email, pass) {
        for (var i = 0; i < $scope.user.length; i++) {
            if ($scope.user[i].Email == email && $scope.user[i].Password == pass) {
                return $scope.user[i];
            }
        }
        return false;
    }


    $http.get('data/songs.json').then(function (res) {
        $scope.songs = res.data;
    });

    $http.get('data/artists.json').then(function (res) {
        $scope.artists = res.data;
    });

    $http.get('data/new_releases.json').then(function (res) {
        $scope.new_releases = res.data;
    });

    $http.get('data/albums.json').then(function (res) {
        $scope.albums = res.data;
    });

    $http.get('data/comments.json').then(function (res) {
        $scope.comments = res.data;
    });
 });



 app.directive('passwordMatch', [function(){
	return {
		restrict: 'A',
		scope:true,
		require: 'ngModel',
		link: function (scope, elem , attrs,control) {
			var checker = function () {
				var e1 = scope.$eval(attrs.ngModel);
				var e2 = scope.$eval(attrs.passwordMatch);
				return e1 == e2;
			};
			scope.$watch(checker, function (n) {
				control.$setValidity("unique", n);
			});
		}
	};
}]);

app.directive("owlCarousel",['$timeout',function($timeout) {
    return {
        restrict: 'E',
        transclude: false,
        link: function (scope) {
            scope.initCarousel = function(element) {
                $timeout(function () { 
                    // provide any default options you want
                var defaultOptions = {
                    smartSpeed: 600,
                    nav: true,
                    loop: true,
                    margin: 10,
                    navText: ["<i style='font-size: 20px' class='fa fa-chevron-left' aria-hidden='true'></i>", "<i style='font-size: 20px' class='fa fa-chevron-right' aria-hidden='true'></i>"],
                    responsive: {
                        0: {
                            items: 1
                        },
                        767: {
                            items: 2
                        },
                        1023: {
                            items: 4,
                            margin: 30,
                        }
                    }
                };
                var customOptions = scope.$eval($(element).attr('data-options'));
                // combine the two options objects
                for(var key in customOptions) {
                    defaultOptions[key] = customOptions[key];
                }
                
                // init carousel
                $(element).owlCarousel(defaultOptions);
                 },50);
              
            };
        }
    };
}]);
app.directive('owlCarouselItem', [function() {
    return {
        restrict: 'A',
        transclude: false,
        link: function(scope, element) {
            if(scope.$last) {
                scope.initCarousel(element.parent());
            }
        }
    };
}]);