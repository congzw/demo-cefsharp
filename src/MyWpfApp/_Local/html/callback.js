(function (window, $) {
    'use strict';
    var show = function (message) {
        var $message = $("#message");
        var theDate = new Date().toLocaleString();
        $message.html('<h2>js call: ' + message + ' => ' + theDate + '</h2>');
        console.log('js call: ' + message);
    };
    var theVo = window.theVo;
    window.myVo = function() {

        var message = '';
        var callTimes = 0;

        var successFunc = function (messageResult) {
            message = messageResult.Message;
            show(message + (++callTimes));
        }

        var failFunc = function (messageResult) {
            message = messageResult.Message;
            show(message + (++callTimes));
        }

        return {
            foo: function (shouldSuccess) {
                theVo.fooWithCallback({ ShouldSuccess: shouldSuccess }, successFunc, failFunc);
            },
            Foo: function (shouldSuccess) {
                theVo.FooWithCallback({ ShouldSuccess: shouldSuccess }, successFunc, failFunc);
            },
            blah: function () {
                theVo.blah({ Name: 'abc' });
            },
            blah2: function () {
                theVo.blah({ Name: 'abc' }, successFunc);
            },
            blah3: function () {
                theVo.blah({ Name: 'abc' }, successFunc, failFunc);
            }
        };

    }();

}(window, jQuery));