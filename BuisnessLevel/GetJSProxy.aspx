<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GetJSProxy.aspx.cs" Inherits="BL.GetJSProxy" %>


function ProxyJsonpExecutor(callbackFunctionExec) {
    this.isExecuted = false;
    this.userFunction = callbackFunctionExec;

    this.Callback = function (data) {
        this.isExecuted = true;
        this.userFunction(data);
    }
}


function ProxyJsonp() {
    this.id = '';
    this.baseUrl = '<%= baseUrl %>';

    this.checkCallBack = function (callbackObjName, callbackError) {

        if ('undefined' != typeof window[callbackObjName]) {

            var target = window[callbackObjName];

            if (target.isExecuted) {

                try {
                    delete window[callbackObjName]
                } catch (e) {
                    window[callbackObjName] = null;
                }
            }
            else {
                if ('undefined' != typeof callbackError) {
                    callbackError();
                }
            }
        }
        else {
            if ('undefined' != typeof callbackError) {
                callbackError();
            }
        }

    }


    // GUID part generator
    this.s4 = function () {
        return Math.floor((1 + Math.random()) * 0x10000)
                   .toString(16)
                   .substring(1);
    }


    this.executeServerMethod = function(methodName, paramString, regimCss, callbackSuccess, callbackError) {

        var that = this;
        var thatId = this.s4() + this.s4();

        var thatExecutorObjectName = methodName + thatId;

        var regCssParam = '1';
        if ('undefined' != typeof regimCss) {
            regCssParam = regimCss;
        }

        window[thatExecutorObjectName] = new ProxyJsonpExecutor(callbackSuccess);

        var script = document.createElement('script');
        script.type = 'text/javascript';
        script.src = this.baseUrl + methodName + '.aspx' +
                     '?userid=' + this.id +
                     paramString +
                     '&cssreg=' + regCssParam +
                     '&callback=' + thatExecutorObjectName;

        if (script.readyState) {  //IE
            script.onreadystatechange = function () {
                if (script.readyState == "loaded" || script.readyState == "complete") {
                    script.onreadystatechange = null;
                    setTimeout(that.checkCallBack(thatExecutorObjectName, callbackError), 0);
                }
            };
        } else {  //Others
            script.onload =  script.onerror = function () {
                that.checkCallBack(thatExecutorObjectName, callbackError);
            };
        }

        document.getElementsByTagName('head')[0].appendChild(script);
    }


    this.LoadJS = function(src, callback) {
        var script = document.createElement('script'),
            loaded;
        script.setAttribute('src', src);
        if (callback) {
          script.onreadystatechange = script.onload = function() {
            if (!loaded) {
              callback();
            }
            loaded = true;
          };
        }
        document.getElementsByTagName('head')[0].appendChild(script);
    }



    this.GetAddrData = function (address, regimCss, callbackSuccess, callbackError) {

        var paramStr = '&addr=' + encodeURIComponent(address);

        this.executeServerMethod('GetSchoolAddressData', paramStr, regimCss, callbackSuccess, callbackError);
    }


    this.GetData = function (schoolnum, regimCss, callbackSuccess, callbackError) {

        var paramStr = '&num=' + encodeURIComponent(schoolnum);

        this.executeServerMethod('GetSchoolData', paramStr, regimCss, callbackSuccess, callbackError);
    }

    
    this.GetMapData = function (regimCss, callbackSuccess, callbackError) {

        this.executeServerMethod('GetSchoolMapData', '', regimCss, callbackSuccess, callbackError);
    }


    this.GetCoordData = function (attitude, longtitude) {

	      var script = document.createElement("script");
	      script.type = "text/javascript";
	      script.src = this.baseUrl + "GetSchoolCoordMapData.aspx" + "?la=" + attitude + "&lg=" + longtitude;
	      document.getElementsByTagName("head")[0].appendChild(script);
    }


}


var schoolDataProxy = new ProxyJsonp();