// Android 2.3 上支持的 Function.prototype.bind() 的填充代码
(function () {
    if (!Function.prototype.bind) {
        Function.prototype.bind = function (thisValue) {
            if (typeof this !== "function") {
                throw new TypeError(this + " cannot be bound as it is not a function");
            }

            // bind() 还允许预挂起调用的参数
            var preArgs = Array.prototype.slice.call(arguments, 1);

            //要对其绑定“this”值和参数的实际函数
            var functionToBind = this;
            var noOpFunction = function () { };

            // 要使用的“this”参数
            var thisArg = this instanceof noOpFunction && thisValue ? this : thisValue;

            // 产生的绑定函数
            var boundFunction = function () {
                return functionToBind.apply(thisArg, preArgs.concat(Array.prototype.slice.call(arguments)));
            };

            noOpFunction.prototype = this.prototype;
            boundFunction.prototype = new noOpFunction();

            return boundFunction;
        };
    }
}());
