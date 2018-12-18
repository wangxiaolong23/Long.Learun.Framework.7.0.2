/*!
* =====================================================
*Learun MobileUI v1.0.0
* =====================================================
*/
/* NUGET: BEGIN LICENSE TEXT
 *
 * Microsoft grants you the right to use these script files for the sole
 * purpose of either: (i) interacting through your browser with the Microsoft
 * website or online service, subject to the applicable licensing or use
 * terms; or (ii) using the files as included with a Microsoft product subject
 * to that product's license terms. Microsoft reserves all other rights to the
 * files not expressly granted by Microsoft, whether by implication, estoppel
 * or otherwise. Insofar as a script file is dual licensed under GPL,
 * Microsoft neither took the code under GPL nor distributes it thereunder but
 * under the terms set out in this paragraph. All notices and licenses
 * below are for informational purposes only.
 *
 * JQUERY CORE 1.10.2; Copyright 2005, 2013 jQuery Foundation, Inc. and other contributors; http://jquery.org/license
 * Includes Sizzle.js; Copyright 2013 jQuery Foundation, Inc. and other contributors; http://opensource.org/licenses/MIT
 *
 * NUGET: END LICENSE TEXT */
/*! jQuery v1.10.2 | (c) 2005, 2013 jQuery Foundation, Inc. | jquery.org/license
//@ sourceMappingURL=jquery-1.10.2.min.map
*/
(function(e,t){var n,r,i=typeof t,o=e.location,a=e.document,s=a.documentElement,l=e.jQuery,u=e.$,c={},p=[],f="1.10.2",d=p.concat,h=p.push,g=p.slice,m=p.indexOf,y=c.toString,v=c.hasOwnProperty,b=f.trim,x=function(e,t){return new x.fn.init(e,t,r)},w=/[+-]?(?:\d*\.|)\d+(?:[eE][+-]?\d+|)/.source,T=/\S+/g,C=/^[\s\uFEFF\xA0]+|[\s\uFEFF\xA0]+$/g,N=/^(?:\s*(<[\w\W]+>)[^>]*|#([\w-]*))$/,k=/^<(\w+)\s*\/?>(?:<\/\1>|)$/,E=/^[\],:{}\s]*$/,S=/(?:^|:|,)(?:\s*\[)+/g,A=/\\(?:["\\\/bfnrt]|u[\da-fA-F]{4})/g,j=/"[^"\\\r\n]*"|true|false|null|-?(?:\d+\.|)\d+(?:[eE][+-]?\d+|)/g,D=/^-ms-/,L=/-([\da-z])/gi,H=function(e,t){return t.toUpperCase()},q=function(e){(a.addEventListener||"load"===e.type||"complete"===a.readyState)&&(_(),x.ready())},_=function(){a.addEventListener?(a.removeEventListener("DOMContentLoaded",q,!1),e.removeEventListener("load",q,!1)):(a.detachEvent("onreadystatechange",q),e.detachEvent("onload",q))};x.fn=x.prototype={jquery:f,constructor:x,init:function(e,n,r){var i,o;if(!e)return this;if("string"==typeof e){if(i="<"===e.charAt(0)&&">"===e.charAt(e.length-1)&&e.length>=3?[null,e,null]:N.exec(e),!i||!i[1]&&n)return!n||n.jquery?(n||r).find(e):this.constructor(n).find(e);if(i[1]){if(n=n instanceof x?n[0]:n,x.merge(this,x.parseHTML(i[1],n&&n.nodeType?n.ownerDocument||n:a,!0)),k.test(i[1])&&x.isPlainObject(n))for(i in n)x.isFunction(this[i])?this[i](n[i]):this.attr(i,n[i]);return this}if(o=a.getElementById(i[2]),o&&o.parentNode){if(o.id!==i[2])return r.find(e);this.length=1,this[0]=o}return this.context=a,this.selector=e,this}return e.nodeType?(this.context=this[0]=e,this.length=1,this):x.isFunction(e)?r.ready(e):(e.selector!==t&&(this.selector=e.selector,this.context=e.context),x.makeArray(e,this))},selector:"",length:0,toArray:function(){return g.call(this)},get:function(e){return null==e?this.toArray():0>e?this[this.length+e]:this[e]},pushStack:function(e){var t=x.merge(this.constructor(),e);return t.prevObject=this,t.context=this.context,t},each:function(e,t){return x.each(this,e,t)},ready:function(e){return x.ready.promise().done(e),this},slice:function(){return this.pushStack(g.apply(this,arguments))},first:function(){return this.eq(0)},last:function(){return this.eq(-1)},eq:function(e){var t=this.length,n=+e+(0>e?t:0);return this.pushStack(n>=0&&t>n?[this[n]]:[])},map:function(e){return this.pushStack(x.map(this,function(t,n){return e.call(t,n,t)}))},end:function(){return this.prevObject||this.constructor(null)},push:h,sort:[].sort,splice:[].splice},x.fn.init.prototype=x.fn,x.extend=x.fn.extend=function(){var e,n,r,i,o,a,s=arguments[0]||{},l=1,u=arguments.length,c=!1;for("boolean"==typeof s&&(c=s,s=arguments[1]||{},l=2),"object"==typeof s||x.isFunction(s)||(s={}),u===l&&(s=this,--l);u>l;l++)if(null!=(o=arguments[l]))for(i in o)e=s[i],r=o[i],s!==r&&(c&&r&&(x.isPlainObject(r)||(n=x.isArray(r)))?(n?(n=!1,a=e&&x.isArray(e)?e:[]):a=e&&x.isPlainObject(e)?e:{},s[i]=x.extend(c,a,r)):r!==t&&(s[i]=r));return s},x.extend({expando:"jQuery"+(f+Math.random()).replace(/\D/g,""),noConflict:function(t){return e.$===x&&(e.$=u),t&&e.jQuery===x&&(e.jQuery=l),x},isReady:!1,readyWait:1,holdReady:function(e){e?x.readyWait++:x.ready(!0)},ready:function(e){if(e===!0?!--x.readyWait:!x.isReady){if(!a.body)return setTimeout(x.ready);x.isReady=!0,e!==!0&&--x.readyWait>0||(n.resolveWith(a,[x]),x.fn.trigger&&x(a).trigger("ready").off("ready"))}},isFunction:function(e){return"function"===x.type(e)},isArray:Array.isArray||function(e){return"array"===x.type(e)},isWindow:function(e){return null!=e&&e==e.window},isNumeric:function(e){return!isNaN(parseFloat(e))&&isFinite(e)},type:function(e){return null==e?e+"":"object"==typeof e||"function"==typeof e?c[y.call(e)]||"object":typeof e},isPlainObject:function(e){var n;if(!e||"object"!==x.type(e)||e.nodeType||x.isWindow(e))return!1;try{if(e.constructor&&!v.call(e,"constructor")&&!v.call(e.constructor.prototype,"isPrototypeOf"))return!1}catch(r){return!1}if(x.support.ownLast)for(n in e)return v.call(e,n);for(n in e);return n===t||v.call(e,n)},isEmptyObject:function(e){var t;for(t in e)return!1;return!0},error:function(e){throw Error(e)},parseHTML:function(e,t,n){if(!e||"string"!=typeof e)return null;"boolean"==typeof t&&(n=t,t=!1),t=t||a;var r=k.exec(e),i=!n&&[];return r?[t.createElement(r[1])]:(r=x.buildFragment([e],t,i),i&&x(i).remove(),x.merge([],r.childNodes))},parseJSON:function(n){return e.JSON&&e.JSON.parse?e.JSON.parse(n):null===n?n:"string"==typeof n&&(n=x.trim(n),n&&E.test(n.replace(A,"@").replace(j,"]").replace(S,"")))?Function("return "+n)():(x.error("Invalid JSON: "+n),t)},parseXML:function(n){var r,i;if(!n||"string"!=typeof n)return null;try{e.DOMParser?(i=new DOMParser,r=i.parseFromString(n,"text/xml")):(r=new ActiveXObject("Microsoft.XMLDOM"),r.async="false",r.loadXML(n))}catch(o){r=t}return r&&r.documentElement&&!r.getElementsByTagName("parsererror").length||x.error("Invalid XML: "+n),r},noop:function(){},globalEval:function(t){t&&x.trim(t)&&(e.execScript||function(t){e.eval.call(e,t)})(t)},camelCase:function(e){return e.replace(D,"ms-").replace(L,H)},nodeName:function(e,t){return e.nodeName&&e.nodeName.toLowerCase()===t.toLowerCase()},each:function(e,t,n){var r,i=0,o=e.length,a=M(e);if(n){if(a){for(;o>i;i++)if(r=t.apply(e[i],n),r===!1)break}else for(i in e)if(r=t.apply(e[i],n),r===!1)break}else if(a){for(;o>i;i++)if(r=t.call(e[i],i,e[i]),r===!1)break}else for(i in e)if(r=t.call(e[i],i,e[i]),r===!1)break;return e},trim:b&&!b.call("\ufeff\u00a0")?function(e){return null==e?"":b.call(e)}:function(e){return null==e?"":(e+"").replace(C,"")},makeArray:function(e,t){var n=t||[];return null!=e&&(M(Object(e))?x.merge(n,"string"==typeof e?[e]:e):h.call(n,e)),n},inArray:function(e,t,n){var r;if(t){if(m)return m.call(t,e,n);for(r=t.length,n=n?0>n?Math.max(0,r+n):n:0;r>n;n++)if(n in t&&t[n]===e)return n}return-1},merge:function(e,n){var r=n.length,i=e.length,o=0;if("number"==typeof r)for(;r>o;o++)e[i++]=n[o];else while(n[o]!==t)e[i++]=n[o++];return e.length=i,e},grep:function(e,t,n){var r,i=[],o=0,a=e.length;for(n=!!n;a>o;o++)r=!!t(e[o],o),n!==r&&i.push(e[o]);return i},map:function(e,t,n){var r,i=0,o=e.length,a=M(e),s=[];if(a)for(;o>i;i++)r=t(e[i],i,n),null!=r&&(s[s.length]=r);else for(i in e)r=t(e[i],i,n),null!=r&&(s[s.length]=r);return d.apply([],s)},guid:1,proxy:function(e,n){var r,i,o;return"string"==typeof n&&(o=e[n],n=e,e=o),x.isFunction(e)?(r=g.call(arguments,2),i=function(){return e.apply(n||this,r.concat(g.call(arguments)))},i.guid=e.guid=e.guid||x.guid++,i):t},access:function(e,n,r,i,o,a,s){var l=0,u=e.length,c=null==r;if("object"===x.type(r)){o=!0;for(l in r)x.access(e,n,l,r[l],!0,a,s)}else if(i!==t&&(o=!0,x.isFunction(i)||(s=!0),c&&(s?(n.call(e,i),n=null):(c=n,n=function(e,t,n){return c.call(x(e),n)})),n))for(;u>l;l++)n(e[l],r,s?i:i.call(e[l],l,n(e[l],r)));return o?e:c?n.call(e):u?n(e[0],r):a},now:function(){return(new Date).getTime()},swap:function(e,t,n,r){var i,o,a={};for(o in t)a[o]=e.style[o],e.style[o]=t[o];i=n.apply(e,r||[]);for(o in t)e.style[o]=a[o];return i}}),x.ready.promise=function(t){if(!n)if(n=x.Deferred(),"complete"===a.readyState)setTimeout(x.ready);else if(a.addEventListener)a.addEventListener("DOMContentLoaded",q,!1),e.addEventListener("load",q,!1);else{a.attachEvent("onreadystatechange",q),e.attachEvent("onload",q);var r=!1;try{r=null==e.frameElement&&a.documentElement}catch(i){}r&&r.doScroll&&function o(){if(!x.isReady){try{r.doScroll("left")}catch(e){return setTimeout(o,50)}_(),x.ready()}}()}return n.promise(t)},x.each("Boolean Number String Function Array Date RegExp Object Error".split(" "),function(e,t){c["[object "+t+"]"]=t.toLowerCase()});function M(e){var t=e.length,n=x.type(e);return x.isWindow(e)?!1:1===e.nodeType&&t?!0:"array"===n||"function"!==n&&(0===t||"number"==typeof t&&t>0&&t-1 in e)}r=x(a),function(e,t){var n,r,i,o,a,s,l,u,c,p,f,d,h,g,m,y,v,b="sizzle"+-new Date,w=e.document,T=0,C=0,N=st(),k=st(),E=st(),S=!1,A=function(e,t){return e===t?(S=!0,0):0},j=typeof t,D=1<<31,L={}.hasOwnProperty,H=[],q=H.pop,_=H.push,M=H.push,O=H.slice,F=H.indexOf||function(e){var t=0,n=this.length;for(;n>t;t++)if(this[t]===e)return t;return-1},B="checked|selected|async|autofocus|autoplay|controls|defer|disabled|hidden|ismap|loop|multiple|open|readonly|required|scoped",P="[\\x20\\t\\r\\n\\f]",R="(?:\\\\.|[\\w-]|[^\\x00-\\xa0])+",W=R.replace("w","w#"),$="\\["+P+"*("+R+")"+P+"*(?:([*^$|!~]?=)"+P+"*(?:(['\"])((?:\\\\.|[^\\\\])*?)\\3|("+W+")|)|)"+P+"*\\]",I=":("+R+")(?:\\(((['\"])((?:\\\\.|[^\\\\])*?)\\3|((?:\\\\.|[^\\\\()[\\]]|"+$.replace(3,8)+")*)|.*)\\)|)",z=RegExp("^"+P+"+|((?:^|[^\\\\])(?:\\\\.)*)"+P+"+$","g"),X=RegExp("^"+P+"*,"+P+"*"),U=RegExp("^"+P+"*([>+~]|"+P+")"+P+"*"),V=RegExp(P+"*[+~]"),Y=RegExp("="+P+"*([^\\]'\"]*)"+P+"*\\]","g"),J=RegExp(I),G=RegExp("^"+W+"$"),Q={ID:RegExp("^#("+R+")"),CLASS:RegExp("^\\.("+R+")"),TAG:RegExp("^("+R.replace("w","w*")+")"),ATTR:RegExp("^"+$),PSEUDO:RegExp("^"+I),CHILD:RegExp("^:(only|first|last|nth|nth-last)-(child|of-type)(?:\\("+P+"*(even|odd|(([+-]|)(\\d*)n|)"+P+"*(?:([+-]|)"+P+"*(\\d+)|))"+P+"*\\)|)","i"),bool:RegExp("^(?:"+B+")$","i"),needsContext:RegExp("^"+P+"*[>+~]|:(even|odd|eq|gt|lt|nth|first|last)(?:\\("+P+"*((?:-\\d)?\\d*)"+P+"*\\)|)(?=[^-]|$)","i")},K=/^[^{]+\{\s*\[native \w/,Z=/^(?:#([\w-]+)|(\w+)|\.([\w-]+))$/,et=/^(?:input|select|textarea|button)$/i,tt=/^h\d$/i,nt=/'|\\/g,rt=RegExp("\\\\([\\da-f]{1,6}"+P+"?|("+P+")|.)","ig"),it=function(e,t,n){var r="0x"+t-65536;return r!==r||n?t:0>r?String.fromCharCode(r+65536):String.fromCharCode(55296|r>>10,56320|1023&r)};try{M.apply(H=O.call(w.childNodes),w.childNodes),H[w.childNodes.length].nodeType}catch(ot){M={apply:H.length?function(e,t){_.apply(e,O.call(t))}:function(e,t){var n=e.length,r=0;while(e[n++]=t[r++]);e.length=n-1}}}function at(e,t,n,i){var o,a,s,l,u,c,d,m,y,x;if((t?t.ownerDocument||t:w)!==f&&p(t),t=t||f,n=n||[],!e||"string"!=typeof e)return n;if(1!==(l=t.nodeType)&&9!==l)return[];if(h&&!i){if(o=Z.exec(e))if(s=o[1]){if(9===l){if(a=t.getElementById(s),!a||!a.parentNode)return n;if(a.id===s)return n.push(a),n}else if(t.ownerDocument&&(a=t.ownerDocument.getElementById(s))&&v(t,a)&&a.id===s)return n.push(a),n}else{if(o[2])return M.apply(n,t.getElementsByTagName(e)),n;if((s=o[3])&&r.getElementsByClassName&&t.getElementsByClassName)return M.apply(n,t.getElementsByClassName(s)),n}if(r.qsa&&(!g||!g.test(e))){if(m=d=b,y=t,x=9===l&&e,1===l&&"object"!==t.nodeName.toLowerCase()){c=mt(e),(d=t.getAttribute("id"))?m=d.replace(nt,"\\$&"):t.setAttribute("id",m),m="[id='"+m+"'] ",u=c.length;while(u--)c[u]=m+yt(c[u]);y=V.test(e)&&t.parentNode||t,x=c.join(",")}if(x)try{return M.apply(n,y.querySelectorAll(x)),n}catch(T){}finally{d||t.removeAttribute("id")}}}return kt(e.replace(z,"$1"),t,n,i)}function st(){var e=[];function t(n,r){return e.push(n+=" ")>o.cacheLength&&delete t[e.shift()],t[n]=r}return t}function lt(e){return e[b]=!0,e}function ut(e){var t=f.createElement("div");try{return!!e(t)}catch(n){return!1}finally{t.parentNode&&t.parentNode.removeChild(t),t=null}}function ct(e,t){var n=e.split("|"),r=e.length;while(r--)o.attrHandle[n[r]]=t}function pt(e,t){var n=t&&e,r=n&&1===e.nodeType&&1===t.nodeType&&(~t.sourceIndex||D)-(~e.sourceIndex||D);if(r)return r;if(n)while(n=n.nextSibling)if(n===t)return-1;return e?1:-1}function ft(e){return function(t){var n=t.nodeName.toLowerCase();return"input"===n&&t.type===e}}function dt(e){return function(t){var n=t.nodeName.toLowerCase();return("input"===n||"button"===n)&&t.type===e}}function ht(e){return lt(function(t){return t=+t,lt(function(n,r){var i,o=e([],n.length,t),a=o.length;while(a--)n[i=o[a]]&&(n[i]=!(r[i]=n[i]))})})}s=at.isXML=function(e){var t=e&&(e.ownerDocument||e).documentElement;return t?"HTML"!==t.nodeName:!1},r=at.support={},p=at.setDocument=function(e){var n=e?e.ownerDocument||e:w,i=n.defaultView;return n!==f&&9===n.nodeType&&n.documentElement?(f=n,d=n.documentElement,h=!s(n),i&&i.attachEvent&&i!==i.top&&i.attachEvent("onbeforeunload",function(){p()}),r.attributes=ut(function(e){return e.className="i",!e.getAttribute("className")}),r.getElementsByTagName=ut(function(e){return e.appendChild(n.createComment("")),!e.getElementsByTagName("*").length}),r.getElementsByClassName=ut(function(e){return e.innerHTML="<div class='a'></div><div class='a i'></div>",e.firstChild.className="i",2===e.getElementsByClassName("i").length}),r.getById=ut(function(e){return d.appendChild(e).id=b,!n.getElementsByName||!n.getElementsByName(b).length}),r.getById?(o.find.ID=function(e,t){if(typeof t.getElementById!==j&&h){var n=t.getElementById(e);return n&&n.parentNode?[n]:[]}},o.filter.ID=function(e){var t=e.replace(rt,it);return function(e){return e.getAttribute("id")===t}}):(delete o.find.ID,o.filter.ID=function(e){var t=e.replace(rt,it);return function(e){var n=typeof e.getAttributeNode!==j&&e.getAttributeNode("id");return n&&n.value===t}}),o.find.TAG=r.getElementsByTagName?function(e,n){return typeof n.getElementsByTagName!==j?n.getElementsByTagName(e):t}:function(e,t){var n,r=[],i=0,o=t.getElementsByTagName(e);if("*"===e){while(n=o[i++])1===n.nodeType&&r.push(n);return r}return o},o.find.CLASS=r.getElementsByClassName&&function(e,n){return typeof n.getElementsByClassName!==j&&h?n.getElementsByClassName(e):t},m=[],g=[],(r.qsa=K.test(n.querySelectorAll))&&(ut(function(e){e.innerHTML="<select><option selected=''></option></select>",e.querySelectorAll("[selected]").length||g.push("\\["+P+"*(?:value|"+B+")"),e.querySelectorAll(":checked").length||g.push(":checked")}),ut(function(e){var t=n.createElement("input");t.setAttribute("type","hidden"),e.appendChild(t).setAttribute("t",""),e.querySelectorAll("[t^='']").length&&g.push("[*^$]="+P+"*(?:''|\"\")"),e.querySelectorAll(":enabled").length||g.push(":enabled",":disabled"),e.querySelectorAll("*,:x"),g.push(",.*:")})),(r.matchesSelector=K.test(y=d.webkitMatchesSelector||d.mozMatchesSelector||d.oMatchesSelector||d.msMatchesSelector))&&ut(function(e){r.disconnectedMatch=y.call(e,"div"),y.call(e,"[s!='']:x"),m.push("!=",I)}),g=g.length&&RegExp(g.join("|")),m=m.length&&RegExp(m.join("|")),v=K.test(d.contains)||d.compareDocumentPosition?function(e,t){var n=9===e.nodeType?e.documentElement:e,r=t&&t.parentNode;return e===r||!(!r||1!==r.nodeType||!(n.contains?n.contains(r):e.compareDocumentPosition&&16&e.compareDocumentPosition(r)))}:function(e,t){if(t)while(t=t.parentNode)if(t===e)return!0;return!1},A=d.compareDocumentPosition?function(e,t){if(e===t)return S=!0,0;var i=t.compareDocumentPosition&&e.compareDocumentPosition&&e.compareDocumentPosition(t);return i?1&i||!r.sortDetached&&t.compareDocumentPosition(e)===i?e===n||v(w,e)?-1:t===n||v(w,t)?1:c?F.call(c,e)-F.call(c,t):0:4&i?-1:1:e.compareDocumentPosition?-1:1}:function(e,t){var r,i=0,o=e.parentNode,a=t.parentNode,s=[e],l=[t];if(e===t)return S=!0,0;if(!o||!a)return e===n?-1:t===n?1:o?-1:a?1:c?F.call(c,e)-F.call(c,t):0;if(o===a)return pt(e,t);r=e;while(r=r.parentNode)s.unshift(r);r=t;while(r=r.parentNode)l.unshift(r);while(s[i]===l[i])i++;return i?pt(s[i],l[i]):s[i]===w?-1:l[i]===w?1:0},n):f},at.matches=function(e,t){return at(e,null,null,t)},at.matchesSelector=function(e,t){if((e.ownerDocument||e)!==f&&p(e),t=t.replace(Y,"='$1']"),!(!r.matchesSelector||!h||m&&m.test(t)||g&&g.test(t)))try{var n=y.call(e,t);if(n||r.disconnectedMatch||e.document&&11!==e.document.nodeType)return n}catch(i){}return at(t,f,null,[e]).length>0},at.contains=function(e,t){return(e.ownerDocument||e)!==f&&p(e),v(e,t)},at.attr=function(e,n){(e.ownerDocument||e)!==f&&p(e);var i=o.attrHandle[n.toLowerCase()],a=i&&L.call(o.attrHandle,n.toLowerCase())?i(e,n,!h):t;return a===t?r.attributes||!h?e.getAttribute(n):(a=e.getAttributeNode(n))&&a.specified?a.value:null:a},at.error=function(e){throw Error("Syntax error, unrecognized expression: "+e)},at.uniqueSort=function(e){var t,n=[],i=0,o=0;if(S=!r.detectDuplicates,c=!r.sortStable&&e.slice(0),e.sort(A),S){while(t=e[o++])t===e[o]&&(i=n.push(o));while(i--)e.splice(n[i],1)}return e},a=at.getText=function(e){var t,n="",r=0,i=e.nodeType;if(i){if(1===i||9===i||11===i){if("string"==typeof e.textContent)return e.textContent;for(e=e.firstChild;e;e=e.nextSibling)n+=a(e)}else if(3===i||4===i)return e.nodeValue}else for(;t=e[r];r++)n+=a(t);return n},o=at.selectors={cacheLength:50,createPseudo:lt,match:Q,attrHandle:{},find:{},relative:{">":{dir:"parentNode",first:!0}," ":{dir:"parentNode"},"+":{dir:"previousSibling",first:!0},"~":{dir:"previousSibling"}},preFilter:{ATTR:function(e){return e[1]=e[1].replace(rt,it),e[3]=(e[4]||e[5]||"").replace(rt,it),"~="===e[2]&&(e[3]=" "+e[3]+" "),e.slice(0,4)},CHILD:function(e){return e[1]=e[1].toLowerCase(),"nth"===e[1].slice(0,3)?(e[3]||at.error(e[0]),e[4]=+(e[4]?e[5]+(e[6]||1):2*("even"===e[3]||"odd"===e[3])),e[5]=+(e[7]+e[8]||"odd"===e[3])):e[3]&&at.error(e[0]),e},PSEUDO:function(e){var n,r=!e[5]&&e[2];return Q.CHILD.test(e[0])?null:(e[3]&&e[4]!==t?e[2]=e[4]:r&&J.test(r)&&(n=mt(r,!0))&&(n=r.indexOf(")",r.length-n)-r.length)&&(e[0]=e[0].slice(0,n),e[2]=r.slice(0,n)),e.slice(0,3))}},filter:{TAG:function(e){var t=e.replace(rt,it).toLowerCase();return"*"===e?function(){return!0}:function(e){return e.nodeName&&e.nodeName.toLowerCase()===t}},CLASS:function(e){var t=N[e+" "];return t||(t=RegExp("(^|"+P+")"+e+"("+P+"|$)"))&&N(e,function(e){return t.test("string"==typeof e.className&&e.className||typeof e.getAttribute!==j&&e.getAttribute("class")||"")})},ATTR:function(e,t,n){return function(r){var i=at.attr(r,e);return null==i?"!="===t:t?(i+="","="===t?i===n:"!="===t?i!==n:"^="===t?n&&0===i.indexOf(n):"*="===t?n&&i.indexOf(n)>-1:"$="===t?n&&i.slice(-n.length)===n:"~="===t?(" "+i+" ").indexOf(n)>-1:"|="===t?i===n||i.slice(0,n.length+1)===n+"-":!1):!0}},CHILD:function(e,t,n,r,i){var o="nth"!==e.slice(0,3),a="last"!==e.slice(-4),s="of-type"===t;return 1===r&&0===i?function(e){return!!e.parentNode}:function(t,n,l){var u,c,p,f,d,h,g=o!==a?"nextSibling":"previousSibling",m=t.parentNode,y=s&&t.nodeName.toLowerCase(),v=!l&&!s;if(m){if(o){while(g){p=t;while(p=p[g])if(s?p.nodeName.toLowerCase()===y:1===p.nodeType)return!1;h=g="only"===e&&!h&&"nextSibling"}return!0}if(h=[a?m.firstChild:m.lastChild],a&&v){c=m[b]||(m[b]={}),u=c[e]||[],d=u[0]===T&&u[1],f=u[0]===T&&u[2],p=d&&m.childNodes[d];while(p=++d&&p&&p[g]||(f=d=0)||h.pop())if(1===p.nodeType&&++f&&p===t){c[e]=[T,d,f];break}}else if(v&&(u=(t[b]||(t[b]={}))[e])&&u[0]===T)f=u[1];else while(p=++d&&p&&p[g]||(f=d=0)||h.pop())if((s?p.nodeName.toLowerCase()===y:1===p.nodeType)&&++f&&(v&&((p[b]||(p[b]={}))[e]=[T,f]),p===t))break;return f-=i,f===r||0===f%r&&f/r>=0}}},PSEUDO:function(e,t){var n,r=o.pseudos[e]||o.setFilters[e.toLowerCase()]||at.error("unsupported pseudo: "+e);return r[b]?r(t):r.length>1?(n=[e,e,"",t],o.setFilters.hasOwnProperty(e.toLowerCase())?lt(function(e,n){var i,o=r(e,t),a=o.length;while(a--)i=F.call(e,o[a]),e[i]=!(n[i]=o[a])}):function(e){return r(e,0,n)}):r}},pseudos:{not:lt(function(e){var t=[],n=[],r=l(e.replace(z,"$1"));return r[b]?lt(function(e,t,n,i){var o,a=r(e,null,i,[]),s=e.length;while(s--)(o=a[s])&&(e[s]=!(t[s]=o))}):function(e,i,o){return t[0]=e,r(t,null,o,n),!n.pop()}}),has:lt(function(e){return function(t){return at(e,t).length>0}}),contains:lt(function(e){return function(t){return(t.textContent||t.innerText||a(t)).indexOf(e)>-1}}),lang:lt(function(e){return G.test(e||"")||at.error("unsupported lang: "+e),e=e.replace(rt,it).toLowerCase(),function(t){var n;do if(n=h?t.lang:t.getAttribute("xml:lang")||t.getAttribute("lang"))return n=n.toLowerCase(),n===e||0===n.indexOf(e+"-");while((t=t.parentNode)&&1===t.nodeType);return!1}}),target:function(t){var n=e.location&&e.location.hash;return n&&n.slice(1)===t.id},root:function(e){return e===d},focus:function(e){return e===f.activeElement&&(!f.hasFocus||f.hasFocus())&&!!(e.type||e.href||~e.tabIndex)},enabled:function(e){return e.disabled===!1},disabled:function(e){return e.disabled===!0},checked:function(e){var t=e.nodeName.toLowerCase();return"input"===t&&!!e.checked||"option"===t&&!!e.selected},selected:function(e){return e.parentNode&&e.parentNode.selectedIndex,e.selected===!0},empty:function(e){for(e=e.firstChild;e;e=e.nextSibling)if(e.nodeName>"@"||3===e.nodeType||4===e.nodeType)return!1;return!0},parent:function(e){return!o.pseudos.empty(e)},header:function(e){return tt.test(e.nodeName)},input:function(e){return et.test(e.nodeName)},button:function(e){var t=e.nodeName.toLowerCase();return"input"===t&&"button"===e.type||"button"===t},text:function(e){var t;return"input"===e.nodeName.toLowerCase()&&"text"===e.type&&(null==(t=e.getAttribute("type"))||t.toLowerCase()===e.type)},first:ht(function(){return[0]}),last:ht(function(e,t){return[t-1]}),eq:ht(function(e,t,n){return[0>n?n+t:n]}),even:ht(function(e,t){var n=0;for(;t>n;n+=2)e.push(n);return e}),odd:ht(function(e,t){var n=1;for(;t>n;n+=2)e.push(n);return e}),lt:ht(function(e,t,n){var r=0>n?n+t:n;for(;--r>=0;)e.push(r);return e}),gt:ht(function(e,t,n){var r=0>n?n+t:n;for(;t>++r;)e.push(r);return e})}},o.pseudos.nth=o.pseudos.eq;for(n in{radio:!0,checkbox:!0,file:!0,password:!0,image:!0})o.pseudos[n]=ft(n);for(n in{submit:!0,reset:!0})o.pseudos[n]=dt(n);function gt(){}gt.prototype=o.filters=o.pseudos,o.setFilters=new gt;function mt(e,t){var n,r,i,a,s,l,u,c=k[e+" "];if(c)return t?0:c.slice(0);s=e,l=[],u=o.preFilter;while(s){(!n||(r=X.exec(s)))&&(r&&(s=s.slice(r[0].length)||s),l.push(i=[])),n=!1,(r=U.exec(s))&&(n=r.shift(),i.push({value:n,type:r[0].replace(z," ")}),s=s.slice(n.length));for(a in o.filter)!(r=Q[a].exec(s))||u[a]&&!(r=u[a](r))||(n=r.shift(),i.push({value:n,type:a,matches:r}),s=s.slice(n.length));if(!n)break}return t?s.length:s?at.error(e):k(e,l).slice(0)}function yt(e){var t=0,n=e.length,r="";for(;n>t;t++)r+=e[t].value;return r}function vt(e,t,n){var r=t.dir,o=n&&"parentNode"===r,a=C++;return t.first?function(t,n,i){while(t=t[r])if(1===t.nodeType||o)return e(t,n,i)}:function(t,n,s){var l,u,c,p=T+" "+a;if(s){while(t=t[r])if((1===t.nodeType||o)&&e(t,n,s))return!0}else while(t=t[r])if(1===t.nodeType||o)if(c=t[b]||(t[b]={}),(u=c[r])&&u[0]===p){if((l=u[1])===!0||l===i)return l===!0}else if(u=c[r]=[p],u[1]=e(t,n,s)||i,u[1]===!0)return!0}}function bt(e){return e.length>1?function(t,n,r){var i=e.length;while(i--)if(!e[i](t,n,r))return!1;return!0}:e[0]}function xt(e,t,n,r,i){var o,a=[],s=0,l=e.length,u=null!=t;for(;l>s;s++)(o=e[s])&&(!n||n(o,r,i))&&(a.push(o),u&&t.push(s));return a}function wt(e,t,n,r,i,o){return r&&!r[b]&&(r=wt(r)),i&&!i[b]&&(i=wt(i,o)),lt(function(o,a,s,l){var u,c,p,f=[],d=[],h=a.length,g=o||Nt(t||"*",s.nodeType?[s]:s,[]),m=!e||!o&&t?g:xt(g,f,e,s,l),y=n?i||(o?e:h||r)?[]:a:m;if(n&&n(m,y,s,l),r){u=xt(y,d),r(u,[],s,l),c=u.length;while(c--)(p=u[c])&&(y[d[c]]=!(m[d[c]]=p))}if(o){if(i||e){if(i){u=[],c=y.length;while(c--)(p=y[c])&&u.push(m[c]=p);i(null,y=[],u,l)}c=y.length;while(c--)(p=y[c])&&(u=i?F.call(o,p):f[c])>-1&&(o[u]=!(a[u]=p))}}else y=xt(y===a?y.splice(h,y.length):y),i?i(null,a,y,l):M.apply(a,y)})}function Tt(e){var t,n,r,i=e.length,a=o.relative[e[0].type],s=a||o.relative[" "],l=a?1:0,c=vt(function(e){return e===t},s,!0),p=vt(function(e){return F.call(t,e)>-1},s,!0),f=[function(e,n,r){return!a&&(r||n!==u)||((t=n).nodeType?c(e,n,r):p(e,n,r))}];for(;i>l;l++)if(n=o.relative[e[l].type])f=[vt(bt(f),n)];else{if(n=o.filter[e[l].type].apply(null,e[l].matches),n[b]){for(r=++l;i>r;r++)if(o.relative[e[r].type])break;return wt(l>1&&bt(f),l>1&&yt(e.slice(0,l-1).concat({value:" "===e[l-2].type?"*":""})).replace(z,"$1"),n,r>l&&Tt(e.slice(l,r)),i>r&&Tt(e=e.slice(r)),i>r&&yt(e))}f.push(n)}return bt(f)}function Ct(e,t){var n=0,r=t.length>0,a=e.length>0,s=function(s,l,c,p,d){var h,g,m,y=[],v=0,b="0",x=s&&[],w=null!=d,C=u,N=s||a&&o.find.TAG("*",d&&l.parentNode||l),k=T+=null==C?1:Math.random()||.1;for(w&&(u=l!==f&&l,i=n);null!=(h=N[b]);b++){if(a&&h){g=0;while(m=e[g++])if(m(h,l,c)){p.push(h);break}w&&(T=k,i=++n)}r&&((h=!m&&h)&&v--,s&&x.push(h))}if(v+=b,r&&b!==v){g=0;while(m=t[g++])m(x,y,l,c);if(s){if(v>0)while(b--)x[b]||y[b]||(y[b]=q.call(p));y=xt(y)}M.apply(p,y),w&&!s&&y.length>0&&v+t.length>1&&at.uniqueSort(p)}return w&&(T=k,u=C),x};return r?lt(s):s}l=at.compile=function(e,t){var n,r=[],i=[],o=E[e+" "];if(!o){t||(t=mt(e)),n=t.length;while(n--)o=Tt(t[n]),o[b]?r.push(o):i.push(o);o=E(e,Ct(i,r))}return o};function Nt(e,t,n){var r=0,i=t.length;for(;i>r;r++)at(e,t[r],n);return n}function kt(e,t,n,i){var a,s,u,c,p,f=mt(e);if(!i&&1===f.length){if(s=f[0]=f[0].slice(0),s.length>2&&"ID"===(u=s[0]).type&&r.getById&&9===t.nodeType&&h&&o.relative[s[1].type]){if(t=(o.find.ID(u.matches[0].replace(rt,it),t)||[])[0],!t)return n;e=e.slice(s.shift().value.length)}a=Q.needsContext.test(e)?0:s.length;while(a--){if(u=s[a],o.relative[c=u.type])break;if((p=o.find[c])&&(i=p(u.matches[0].replace(rt,it),V.test(s[0].type)&&t.parentNode||t))){if(s.splice(a,1),e=i.length&&yt(s),!e)return M.apply(n,i),n;break}}}return l(e,f)(i,t,!h,n,V.test(e)),n}r.sortStable=b.split("").sort(A).join("")===b,r.detectDuplicates=S,p(),r.sortDetached=ut(function(e){return 1&e.compareDocumentPosition(f.createElement("div"))}),ut(function(e){return e.innerHTML="<a href='#'></a>","#"===e.firstChild.getAttribute("href")})||ct("type|href|height|width",function(e,n,r){return r?t:e.getAttribute(n,"type"===n.toLowerCase()?1:2)}),r.attributes&&ut(function(e){return e.innerHTML="<input/>",e.firstChild.setAttribute("value",""),""===e.firstChild.getAttribute("value")})||ct("value",function(e,n,r){return r||"input"!==e.nodeName.toLowerCase()?t:e.defaultValue}),ut(function(e){return null==e.getAttribute("disabled")})||ct(B,function(e,n,r){var i;return r?t:(i=e.getAttributeNode(n))&&i.specified?i.value:e[n]===!0?n.toLowerCase():null}),x.find=at,x.expr=at.selectors,x.expr[":"]=x.expr.pseudos,x.unique=at.uniqueSort,x.text=at.getText,x.isXMLDoc=at.isXML,x.contains=at.contains}(e);var O={};function F(e){var t=O[e]={};return x.each(e.match(T)||[],function(e,n){t[n]=!0}),t}x.Callbacks=function(e){e="string"==typeof e?O[e]||F(e):x.extend({},e);var n,r,i,o,a,s,l=[],u=!e.once&&[],c=function(t){for(r=e.memory&&t,i=!0,a=s||0,s=0,o=l.length,n=!0;l&&o>a;a++)if(l[a].apply(t[0],t[1])===!1&&e.stopOnFalse){r=!1;break}n=!1,l&&(u?u.length&&c(u.shift()):r?l=[]:p.disable())},p={add:function(){if(l){var t=l.length;(function i(t){x.each(t,function(t,n){var r=x.type(n);"function"===r?e.unique&&p.has(n)||l.push(n):n&&n.length&&"string"!==r&&i(n)})})(arguments),n?o=l.length:r&&(s=t,c(r))}return this},remove:function(){return l&&x.each(arguments,function(e,t){var r;while((r=x.inArray(t,l,r))>-1)l.splice(r,1),n&&(o>=r&&o--,a>=r&&a--)}),this},has:function(e){return e?x.inArray(e,l)>-1:!(!l||!l.length)},empty:function(){return l=[],o=0,this},disable:function(){return l=u=r=t,this},disabled:function(){return!l},lock:function(){return u=t,r||p.disable(),this},locked:function(){return!u},fireWith:function(e,t){return!l||i&&!u||(t=t||[],t=[e,t.slice?t.slice():t],n?u.push(t):c(t)),this},fire:function(){return p.fireWith(this,arguments),this},fired:function(){return!!i}};return p},x.extend({Deferred:function(e){var t=[["resolve","done",x.Callbacks("once memory"),"resolved"],["reject","fail",x.Callbacks("once memory"),"rejected"],["notify","progress",x.Callbacks("memory")]],n="pending",r={state:function(){return n},always:function(){return i.done(arguments).fail(arguments),this},then:function(){var e=arguments;return x.Deferred(function(n){x.each(t,function(t,o){var a=o[0],s=x.isFunction(e[t])&&e[t];i[o[1]](function(){var e=s&&s.apply(this,arguments);e&&x.isFunction(e.promise)?e.promise().done(n.resolve).fail(n.reject).progress(n.notify):n[a+"With"](this===r?n.promise():this,s?[e]:arguments)})}),e=null}).promise()},promise:function(e){return null!=e?x.extend(e,r):r}},i={};return r.pipe=r.then,x.each(t,function(e,o){var a=o[2],s=o[3];r[o[1]]=a.add,s&&a.add(function(){n=s},t[1^e][2].disable,t[2][2].lock),i[o[0]]=function(){return i[o[0]+"With"](this===i?r:this,arguments),this},i[o[0]+"With"]=a.fireWith}),r.promise(i),e&&e.call(i,i),i},when:function(e){var t=0,n=g.call(arguments),r=n.length,i=1!==r||e&&x.isFunction(e.promise)?r:0,o=1===i?e:x.Deferred(),a=function(e,t,n){return function(r){t[e]=this,n[e]=arguments.length>1?g.call(arguments):r,n===s?o.notifyWith(t,n):--i||o.resolveWith(t,n)}},s,l,u;if(r>1)for(s=Array(r),l=Array(r),u=Array(r);r>t;t++)n[t]&&x.isFunction(n[t].promise)?n[t].promise().done(a(t,u,n)).fail(o.reject).progress(a(t,l,s)):--i;return i||o.resolveWith(u,n),o.promise()}}),x.support=function(t){var n,r,o,s,l,u,c,p,f,d=a.createElement("div");if(d.setAttribute("className","t"),d.innerHTML="  <link/><table></table><a href='/a'>a</a><input type='checkbox'/>",n=d.getElementsByTagName("*")||[],r=d.getElementsByTagName("a")[0],!r||!r.style||!n.length)return t;s=a.createElement("select"),u=s.appendChild(a.createElement("option")),o=d.getElementsByTagName("input")[0],r.style.cssText="top:1px;float:left;opacity:.5",t.getSetAttribute="t"!==d.className,t.leadingWhitespace=3===d.firstChild.nodeType,t.tbody=!d.getElementsByTagName("tbody").length,t.htmlSerialize=!!d.getElementsByTagName("link").length,t.style=/top/.test(r.getAttribute("style")),t.hrefNormalized="/a"===r.getAttribute("href"),t.opacity=/^0.5/.test(r.style.opacity),t.cssFloat=!!r.style.cssFloat,t.checkOn=!!o.value,t.optSelected=u.selected,t.enctype=!!a.createElement("form").enctype,t.html5Clone="<:nav></:nav>"!==a.createElement("nav").cloneNode(!0).outerHTML,t.inlineBlockNeedsLayout=!1,t.shrinkWrapBlocks=!1,t.pixelPosition=!1,t.deleteExpando=!0,t.noCloneEvent=!0,t.reliableMarginRight=!0,t.boxSizingReliable=!0,o.checked=!0,t.noCloneChecked=o.cloneNode(!0).checked,s.disabled=!0,t.optDisabled=!u.disabled;try{delete d.test}catch(h){t.deleteExpando=!1}o=a.createElement("input"),o.setAttribute("value",""),t.input=""===o.getAttribute("value"),o.value="t",o.setAttribute("type","radio"),t.radioValue="t"===o.value,o.setAttribute("checked","t"),o.setAttribute("name","t"),l=a.createDocumentFragment(),l.appendChild(o),t.appendChecked=o.checked,t.checkClone=l.cloneNode(!0).cloneNode(!0).lastChild.checked,d.attachEvent&&(d.attachEvent("onclick",function(){t.noCloneEvent=!1}),d.cloneNode(!0).click());for(f in{submit:!0,change:!0,focusin:!0})d.setAttribute(c="on"+f,"t"),t[f+"Bubbles"]=c in e||d.attributes[c].expando===!1;d.style.backgroundClip="content-box",d.cloneNode(!0).style.backgroundClip="",t.clearCloneStyle="content-box"===d.style.backgroundClip;for(f in x(t))break;return t.ownLast="0"!==f,x(function(){var n,r,o,s="padding:0;margin:0;border:0;display:block;box-sizing:content-box;-moz-box-sizing:content-box;-webkit-box-sizing:content-box;",l=a.getElementsByTagName("body")[0];l&&(n=a.createElement("div"),n.style.cssText="border:0;width:0;height:0;position:absolute;top:0;left:-9999px;margin-top:1px",l.appendChild(n).appendChild(d),d.innerHTML="<table><tr><td></td><td>t</td></tr></table>",o=d.getElementsByTagName("td"),o[0].style.cssText="padding:0;margin:0;border:0;display:none",p=0===o[0].offsetHeight,o[0].style.display="",o[1].style.display="none",t.reliableHiddenOffsets=p&&0===o[0].offsetHeight,d.innerHTML="",d.style.cssText="box-sizing:border-box;-moz-box-sizing:border-box;-webkit-box-sizing:border-box;padding:1px;border:1px;display:block;width:4px;margin-top:1%;position:absolute;top:1%;",x.swap(l,null!=l.style.zoom?{zoom:1}:{},function(){t.boxSizing=4===d.offsetWidth}),e.getComputedStyle&&(t.pixelPosition="1%"!==(e.getComputedStyle(d,null)||{}).top,t.boxSizingReliable="4px"===(e.getComputedStyle(d,null)||{width:"4px"}).width,r=d.appendChild(a.createElement("div")),r.style.cssText=d.style.cssText=s,r.style.marginRight=r.style.width="0",d.style.width="1px",t.reliableMarginRight=!parseFloat((e.getComputedStyle(r,null)||{}).marginRight)),typeof d.style.zoom!==i&&(d.innerHTML="",d.style.cssText=s+"width:1px;padding:1px;display:inline;zoom:1",t.inlineBlockNeedsLayout=3===d.offsetWidth,d.style.display="block",d.innerHTML="<div></div>",d.firstChild.style.width="5px",t.shrinkWrapBlocks=3!==d.offsetWidth,t.inlineBlockNeedsLayout&&(l.style.zoom=1)),l.removeChild(n),n=d=o=r=null)}),n=s=l=u=r=o=null,t
}({});var B=/(?:\{[\s\S]*\}|\[[\s\S]*\])$/,P=/([A-Z])/g;function R(e,n,r,i){if(x.acceptData(e)){var o,a,s=x.expando,l=e.nodeType,u=l?x.cache:e,c=l?e[s]:e[s]&&s;if(c&&u[c]&&(i||u[c].data)||r!==t||"string"!=typeof n)return c||(c=l?e[s]=p.pop()||x.guid++:s),u[c]||(u[c]=l?{}:{toJSON:x.noop}),("object"==typeof n||"function"==typeof n)&&(i?u[c]=x.extend(u[c],n):u[c].data=x.extend(u[c].data,n)),a=u[c],i||(a.data||(a.data={}),a=a.data),r!==t&&(a[x.camelCase(n)]=r),"string"==typeof n?(o=a[n],null==o&&(o=a[x.camelCase(n)])):o=a,o}}function W(e,t,n){if(x.acceptData(e)){var r,i,o=e.nodeType,a=o?x.cache:e,s=o?e[x.expando]:x.expando;if(a[s]){if(t&&(r=n?a[s]:a[s].data)){x.isArray(t)?t=t.concat(x.map(t,x.camelCase)):t in r?t=[t]:(t=x.camelCase(t),t=t in r?[t]:t.split(" ")),i=t.length;while(i--)delete r[t[i]];if(n?!I(r):!x.isEmptyObject(r))return}(n||(delete a[s].data,I(a[s])))&&(o?x.cleanData([e],!0):x.support.deleteExpando||a!=a.window?delete a[s]:a[s]=null)}}}x.extend({cache:{},noData:{applet:!0,embed:!0,object:"clsid:D27CDB6E-AE6D-11cf-96B8-444553540000"},hasData:function(e){return e=e.nodeType?x.cache[e[x.expando]]:e[x.expando],!!e&&!I(e)},data:function(e,t,n){return R(e,t,n)},removeData:function(e,t){return W(e,t)},_data:function(e,t,n){return R(e,t,n,!0)},_removeData:function(e,t){return W(e,t,!0)},acceptData:function(e){if(e.nodeType&&1!==e.nodeType&&9!==e.nodeType)return!1;var t=e.nodeName&&x.noData[e.nodeName.toLowerCase()];return!t||t!==!0&&e.getAttribute("classid")===t}}),x.fn.extend({data:function(e,n){var r,i,o=null,a=0,s=this[0];if(e===t){if(this.length&&(o=x.data(s),1===s.nodeType&&!x._data(s,"parsedAttrs"))){for(r=s.attributes;r.length>a;a++)i=r[a].name,0===i.indexOf("data-")&&(i=x.camelCase(i.slice(5)),$(s,i,o[i]));x._data(s,"parsedAttrs",!0)}return o}return"object"==typeof e?this.each(function(){x.data(this,e)}):arguments.length>1?this.each(function(){x.data(this,e,n)}):s?$(s,e,x.data(s,e)):null},removeData:function(e){return this.each(function(){x.removeData(this,e)})}});function $(e,n,r){if(r===t&&1===e.nodeType){var i="data-"+n.replace(P,"-$1").toLowerCase();if(r=e.getAttribute(i),"string"==typeof r){try{r="true"===r?!0:"false"===r?!1:"null"===r?null:+r+""===r?+r:B.test(r)?x.parseJSON(r):r}catch(o){}x.data(e,n,r)}else r=t}return r}function I(e){var t;for(t in e)if(("data"!==t||!x.isEmptyObject(e[t]))&&"toJSON"!==t)return!1;return!0}x.extend({queue:function(e,n,r){var i;return e?(n=(n||"fx")+"queue",i=x._data(e,n),r&&(!i||x.isArray(r)?i=x._data(e,n,x.makeArray(r)):i.push(r)),i||[]):t},dequeue:function(e,t){t=t||"fx";var n=x.queue(e,t),r=n.length,i=n.shift(),o=x._queueHooks(e,t),a=function(){x.dequeue(e,t)};"inprogress"===i&&(i=n.shift(),r--),i&&("fx"===t&&n.unshift("inprogress"),delete o.stop,i.call(e,a,o)),!r&&o&&o.empty.fire()},_queueHooks:function(e,t){var n=t+"queueHooks";return x._data(e,n)||x._data(e,n,{empty:x.Callbacks("once memory").add(function(){x._removeData(e,t+"queue"),x._removeData(e,n)})})}}),x.fn.extend({queue:function(e,n){var r=2;return"string"!=typeof e&&(n=e,e="fx",r--),r>arguments.length?x.queue(this[0],e):n===t?this:this.each(function(){var t=x.queue(this,e,n);x._queueHooks(this,e),"fx"===e&&"inprogress"!==t[0]&&x.dequeue(this,e)})},dequeue:function(e){return this.each(function(){x.dequeue(this,e)})},delay:function(e,t){return e=x.fx?x.fx.speeds[e]||e:e,t=t||"fx",this.queue(t,function(t,n){var r=setTimeout(t,e);n.stop=function(){clearTimeout(r)}})},clearQueue:function(e){return this.queue(e||"fx",[])},promise:function(e,n){var r,i=1,o=x.Deferred(),a=this,s=this.length,l=function(){--i||o.resolveWith(a,[a])};"string"!=typeof e&&(n=e,e=t),e=e||"fx";while(s--)r=x._data(a[s],e+"queueHooks"),r&&r.empty&&(i++,r.empty.add(l));return l(),o.promise(n)}});var z,X,U=/[\t\r\n\f]/g,V=/\r/g,Y=/^(?:input|select|textarea|button|object)$/i,J=/^(?:a|area)$/i,G=/^(?:checked|selected)$/i,Q=x.support.getSetAttribute,K=x.support.input;x.fn.extend({attr:function(e,t){return x.access(this,x.attr,e,t,arguments.length>1)},removeAttr:function(e){return this.each(function(){x.removeAttr(this,e)})},prop:function(e,t){return x.access(this,x.prop,e,t,arguments.length>1)},removeProp:function(e){return e=x.propFix[e]||e,this.each(function(){try{this[e]=t,delete this[e]}catch(n){}})},addClass:function(e){var t,n,r,i,o,a=0,s=this.length,l="string"==typeof e&&e;if(x.isFunction(e))return this.each(function(t){x(this).addClass(e.call(this,t,this.className))});if(l)for(t=(e||"").match(T)||[];s>a;a++)if(n=this[a],r=1===n.nodeType&&(n.className?(" "+n.className+" ").replace(U," "):" ")){o=0;while(i=t[o++])0>r.indexOf(" "+i+" ")&&(r+=i+" ");n.className=x.trim(r)}return this},removeClass:function(e){var t,n,r,i,o,a=0,s=this.length,l=0===arguments.length||"string"==typeof e&&e;if(x.isFunction(e))return this.each(function(t){x(this).removeClass(e.call(this,t,this.className))});if(l)for(t=(e||"").match(T)||[];s>a;a++)if(n=this[a],r=1===n.nodeType&&(n.className?(" "+n.className+" ").replace(U," "):"")){o=0;while(i=t[o++])while(r.indexOf(" "+i+" ")>=0)r=r.replace(" "+i+" "," ");n.className=e?x.trim(r):""}return this},toggleClass:function(e,t){var n=typeof e;return"boolean"==typeof t&&"string"===n?t?this.addClass(e):this.removeClass(e):x.isFunction(e)?this.each(function(n){x(this).toggleClass(e.call(this,n,this.className,t),t)}):this.each(function(){if("string"===n){var t,r=0,o=x(this),a=e.match(T)||[];while(t=a[r++])o.hasClass(t)?o.removeClass(t):o.addClass(t)}else(n===i||"boolean"===n)&&(this.className&&x._data(this,"__className__",this.className),this.className=this.className||e===!1?"":x._data(this,"__className__")||"")})},hasClass:function(e){var t=" "+e+" ",n=0,r=this.length;for(;r>n;n++)if(1===this[n].nodeType&&(" "+this[n].className+" ").replace(U," ").indexOf(t)>=0)return!0;return!1},val:function(e){var n,r,i,o=this[0];{if(arguments.length)return i=x.isFunction(e),this.each(function(n){var o;1===this.nodeType&&(o=i?e.call(this,n,x(this).val()):e,null==o?o="":"number"==typeof o?o+="":x.isArray(o)&&(o=x.map(o,function(e){return null==e?"":e+""})),r=x.valHooks[this.type]||x.valHooks[this.nodeName.toLowerCase()],r&&"set"in r&&r.set(this,o,"value")!==t||(this.value=o))});if(o)return r=x.valHooks[o.type]||x.valHooks[o.nodeName.toLowerCase()],r&&"get"in r&&(n=r.get(o,"value"))!==t?n:(n=o.value,"string"==typeof n?n.replace(V,""):null==n?"":n)}}}),x.extend({valHooks:{option:{get:function(e){var t=x.find.attr(e,"value");return null!=t?t:e.text}},select:{get:function(e){var t,n,r=e.options,i=e.selectedIndex,o="select-one"===e.type||0>i,a=o?null:[],s=o?i+1:r.length,l=0>i?s:o?i:0;for(;s>l;l++)if(n=r[l],!(!n.selected&&l!==i||(x.support.optDisabled?n.disabled:null!==n.getAttribute("disabled"))||n.parentNode.disabled&&x.nodeName(n.parentNode,"optgroup"))){if(t=x(n).val(),o)return t;a.push(t)}return a},set:function(e,t){var n,r,i=e.options,o=x.makeArray(t),a=i.length;while(a--)r=i[a],(r.selected=x.inArray(x(r).val(),o)>=0)&&(n=!0);return n||(e.selectedIndex=-1),o}}},attr:function(e,n,r){var o,a,s=e.nodeType;if(e&&3!==s&&8!==s&&2!==s)return typeof e.getAttribute===i?x.prop(e,n,r):(1===s&&x.isXMLDoc(e)||(n=n.toLowerCase(),o=x.attrHooks[n]||(x.expr.match.bool.test(n)?X:z)),r===t?o&&"get"in o&&null!==(a=o.get(e,n))?a:(a=x.find.attr(e,n),null==a?t:a):null!==r?o&&"set"in o&&(a=o.set(e,r,n))!==t?a:(e.setAttribute(n,r+""),r):(x.removeAttr(e,n),t))},removeAttr:function(e,t){var n,r,i=0,o=t&&t.match(T);if(o&&1===e.nodeType)while(n=o[i++])r=x.propFix[n]||n,x.expr.match.bool.test(n)?K&&Q||!G.test(n)?e[r]=!1:e[x.camelCase("default-"+n)]=e[r]=!1:x.attr(e,n,""),e.removeAttribute(Q?n:r)},attrHooks:{type:{set:function(e,t){if(!x.support.radioValue&&"radio"===t&&x.nodeName(e,"input")){var n=e.value;return e.setAttribute("type",t),n&&(e.value=n),t}}}},propFix:{"for":"htmlFor","class":"className"},prop:function(e,n,r){var i,o,a,s=e.nodeType;if(e&&3!==s&&8!==s&&2!==s)return a=1!==s||!x.isXMLDoc(e),a&&(n=x.propFix[n]||n,o=x.propHooks[n]),r!==t?o&&"set"in o&&(i=o.set(e,r,n))!==t?i:e[n]=r:o&&"get"in o&&null!==(i=o.get(e,n))?i:e[n]},propHooks:{tabIndex:{get:function(e){var t=x.find.attr(e,"tabindex");return t?parseInt(t,10):Y.test(e.nodeName)||J.test(e.nodeName)&&e.href?0:-1}}}}),X={set:function(e,t,n){return t===!1?x.removeAttr(e,n):K&&Q||!G.test(n)?e.setAttribute(!Q&&x.propFix[n]||n,n):e[x.camelCase("default-"+n)]=e[n]=!0,n}},x.each(x.expr.match.bool.source.match(/\w+/g),function(e,n){var r=x.expr.attrHandle[n]||x.find.attr;x.expr.attrHandle[n]=K&&Q||!G.test(n)?function(e,n,i){var o=x.expr.attrHandle[n],a=i?t:(x.expr.attrHandle[n]=t)!=r(e,n,i)?n.toLowerCase():null;return x.expr.attrHandle[n]=o,a}:function(e,n,r){return r?t:e[x.camelCase("default-"+n)]?n.toLowerCase():null}}),K&&Q||(x.attrHooks.value={set:function(e,n,r){return x.nodeName(e,"input")?(e.defaultValue=n,t):z&&z.set(e,n,r)}}),Q||(z={set:function(e,n,r){var i=e.getAttributeNode(r);return i||e.setAttributeNode(i=e.ownerDocument.createAttribute(r)),i.value=n+="","value"===r||n===e.getAttribute(r)?n:t}},x.expr.attrHandle.id=x.expr.attrHandle.name=x.expr.attrHandle.coords=function(e,n,r){var i;return r?t:(i=e.getAttributeNode(n))&&""!==i.value?i.value:null},x.valHooks.button={get:function(e,n){var r=e.getAttributeNode(n);return r&&r.specified?r.value:t},set:z.set},x.attrHooks.contenteditable={set:function(e,t,n){z.set(e,""===t?!1:t,n)}},x.each(["width","height"],function(e,n){x.attrHooks[n]={set:function(e,r){return""===r?(e.setAttribute(n,"auto"),r):t}}})),x.support.hrefNormalized||x.each(["href","src"],function(e,t){x.propHooks[t]={get:function(e){return e.getAttribute(t,4)}}}),x.support.style||(x.attrHooks.style={get:function(e){return e.style.cssText||t},set:function(e,t){return e.style.cssText=t+""}}),x.support.optSelected||(x.propHooks.selected={get:function(e){var t=e.parentNode;return t&&(t.selectedIndex,t.parentNode&&t.parentNode.selectedIndex),null}}),x.each(["tabIndex","readOnly","maxLength","cellSpacing","cellPadding","rowSpan","colSpan","useMap","frameBorder","contentEditable"],function(){x.propFix[this.toLowerCase()]=this}),x.support.enctype||(x.propFix.enctype="encoding"),x.each(["radio","checkbox"],function(){x.valHooks[this]={set:function(e,n){return x.isArray(n)?e.checked=x.inArray(x(e).val(),n)>=0:t}},x.support.checkOn||(x.valHooks[this].get=function(e){return null===e.getAttribute("value")?"on":e.value})});var Z=/^(?:input|select|textarea)$/i,et=/^key/,tt=/^(?:mouse|contextmenu)|click/,nt=/^(?:focusinfocus|focusoutblur)$/,rt=/^([^.]*)(?:\.(.+)|)$/;function it(){return!0}function ot(){return!1}function at(){try{return a.activeElement}catch(e){}}x.event={global:{},add:function(e,n,r,o,a){var s,l,u,c,p,f,d,h,g,m,y,v=x._data(e);if(v){r.handler&&(c=r,r=c.handler,a=c.selector),r.guid||(r.guid=x.guid++),(l=v.events)||(l=v.events={}),(f=v.handle)||(f=v.handle=function(e){return typeof x===i||e&&x.event.triggered===e.type?t:x.event.dispatch.apply(f.elem,arguments)},f.elem=e),n=(n||"").match(T)||[""],u=n.length;while(u--)s=rt.exec(n[u])||[],g=y=s[1],m=(s[2]||"").split(".").sort(),g&&(p=x.event.special[g]||{},g=(a?p.delegateType:p.bindType)||g,p=x.event.special[g]||{},d=x.extend({type:g,origType:y,data:o,handler:r,guid:r.guid,selector:a,needsContext:a&&x.expr.match.needsContext.test(a),namespace:m.join(".")},c),(h=l[g])||(h=l[g]=[],h.delegateCount=0,p.setup&&p.setup.call(e,o,m,f)!==!1||(e.addEventListener?e.addEventListener(g,f,!1):e.attachEvent&&e.attachEvent("on"+g,f))),p.add&&(p.add.call(e,d),d.handler.guid||(d.handler.guid=r.guid)),a?h.splice(h.delegateCount++,0,d):h.push(d),x.event.global[g]=!0);e=null}},remove:function(e,t,n,r,i){var o,a,s,l,u,c,p,f,d,h,g,m=x.hasData(e)&&x._data(e);if(m&&(c=m.events)){t=(t||"").match(T)||[""],u=t.length;while(u--)if(s=rt.exec(t[u])||[],d=g=s[1],h=(s[2]||"").split(".").sort(),d){p=x.event.special[d]||{},d=(r?p.delegateType:p.bindType)||d,f=c[d]||[],s=s[2]&&RegExp("(^|\\.)"+h.join("\\.(?:.*\\.|)")+"(\\.|$)"),l=o=f.length;while(o--)a=f[o],!i&&g!==a.origType||n&&n.guid!==a.guid||s&&!s.test(a.namespace)||r&&r!==a.selector&&("**"!==r||!a.selector)||(f.splice(o,1),a.selector&&f.delegateCount--,p.remove&&p.remove.call(e,a));l&&!f.length&&(p.teardown&&p.teardown.call(e,h,m.handle)!==!1||x.removeEvent(e,d,m.handle),delete c[d])}else for(d in c)x.event.remove(e,d+t[u],n,r,!0);x.isEmptyObject(c)&&(delete m.handle,x._removeData(e,"events"))}},trigger:function(n,r,i,o){var s,l,u,c,p,f,d,h=[i||a],g=v.call(n,"type")?n.type:n,m=v.call(n,"namespace")?n.namespace.split("."):[];if(u=f=i=i||a,3!==i.nodeType&&8!==i.nodeType&&!nt.test(g+x.event.triggered)&&(g.indexOf(".")>=0&&(m=g.split("."),g=m.shift(),m.sort()),l=0>g.indexOf(":")&&"on"+g,n=n[x.expando]?n:new x.Event(g,"object"==typeof n&&n),n.isTrigger=o?2:3,n.namespace=m.join("."),n.namespace_re=n.namespace?RegExp("(^|\\.)"+m.join("\\.(?:.*\\.|)")+"(\\.|$)"):null,n.result=t,n.target||(n.target=i),r=null==r?[n]:x.makeArray(r,[n]),p=x.event.special[g]||{},o||!p.trigger||p.trigger.apply(i,r)!==!1)){if(!o&&!p.noBubble&&!x.isWindow(i)){for(c=p.delegateType||g,nt.test(c+g)||(u=u.parentNode);u;u=u.parentNode)h.push(u),f=u;f===(i.ownerDocument||a)&&h.push(f.defaultView||f.parentWindow||e)}d=0;while((u=h[d++])&&!n.isPropagationStopped())n.type=d>1?c:p.bindType||g,s=(x._data(u,"events")||{})[n.type]&&x._data(u,"handle"),s&&s.apply(u,r),s=l&&u[l],s&&x.acceptData(u)&&s.apply&&s.apply(u,r)===!1&&n.preventDefault();if(n.type=g,!o&&!n.isDefaultPrevented()&&(!p._default||p._default.apply(h.pop(),r)===!1)&&x.acceptData(i)&&l&&i[g]&&!x.isWindow(i)){f=i[l],f&&(i[l]=null),x.event.triggered=g;try{i[g]()}catch(y){}x.event.triggered=t,f&&(i[l]=f)}return n.result}},dispatch:function(e){e=x.event.fix(e);var n,r,i,o,a,s=[],l=g.call(arguments),u=(x._data(this,"events")||{})[e.type]||[],c=x.event.special[e.type]||{};if(l[0]=e,e.delegateTarget=this,!c.preDispatch||c.preDispatch.call(this,e)!==!1){s=x.event.handlers.call(this,e,u),n=0;while((o=s[n++])&&!e.isPropagationStopped()){e.currentTarget=o.elem,a=0;while((i=o.handlers[a++])&&!e.isImmediatePropagationStopped())(!e.namespace_re||e.namespace_re.test(i.namespace))&&(e.handleObj=i,e.data=i.data,r=((x.event.special[i.origType]||{}).handle||i.handler).apply(o.elem,l),r!==t&&(e.result=r)===!1&&(e.preventDefault(),e.stopPropagation()))}return c.postDispatch&&c.postDispatch.call(this,e),e.result}},handlers:function(e,n){var r,i,o,a,s=[],l=n.delegateCount,u=e.target;if(l&&u.nodeType&&(!e.button||"click"!==e.type))for(;u!=this;u=u.parentNode||this)if(1===u.nodeType&&(u.disabled!==!0||"click"!==e.type)){for(o=[],a=0;l>a;a++)i=n[a],r=i.selector+" ",o[r]===t&&(o[r]=i.needsContext?x(r,this).index(u)>=0:x.find(r,this,null,[u]).length),o[r]&&o.push(i);o.length&&s.push({elem:u,handlers:o})}return n.length>l&&s.push({elem:this,handlers:n.slice(l)}),s},fix:function(e){if(e[x.expando])return e;var t,n,r,i=e.type,o=e,s=this.fixHooks[i];s||(this.fixHooks[i]=s=tt.test(i)?this.mouseHooks:et.test(i)?this.keyHooks:{}),r=s.props?this.props.concat(s.props):this.props,e=new x.Event(o),t=r.length;while(t--)n=r[t],e[n]=o[n];return e.target||(e.target=o.srcElement||a),3===e.target.nodeType&&(e.target=e.target.parentNode),e.metaKey=!!e.metaKey,s.filter?s.filter(e,o):e},props:"altKey bubbles cancelable ctrlKey currentTarget eventPhase metaKey relatedTarget shiftKey target timeStamp view which".split(" "),fixHooks:{},keyHooks:{props:"char charCode key keyCode".split(" "),filter:function(e,t){return null==e.which&&(e.which=null!=t.charCode?t.charCode:t.keyCode),e}},mouseHooks:{props:"button buttons clientX clientY fromElement offsetX offsetY pageX pageY screenX screenY toElement".split(" "),filter:function(e,n){var r,i,o,s=n.button,l=n.fromElement;return null==e.pageX&&null!=n.clientX&&(i=e.target.ownerDocument||a,o=i.documentElement,r=i.body,e.pageX=n.clientX+(o&&o.scrollLeft||r&&r.scrollLeft||0)-(o&&o.clientLeft||r&&r.clientLeft||0),e.pageY=n.clientY+(o&&o.scrollTop||r&&r.scrollTop||0)-(o&&o.clientTop||r&&r.clientTop||0)),!e.relatedTarget&&l&&(e.relatedTarget=l===e.target?n.toElement:l),e.which||s===t||(e.which=1&s?1:2&s?3:4&s?2:0),e}},special:{load:{noBubble:!0},focus:{trigger:function(){if(this!==at()&&this.focus)try{return this.focus(),!1}catch(e){}},delegateType:"focusin"},blur:{trigger:function(){return this===at()&&this.blur?(this.blur(),!1):t},delegateType:"focusout"},click:{trigger:function(){return x.nodeName(this,"input")&&"checkbox"===this.type&&this.click?(this.click(),!1):t},_default:function(e){return x.nodeName(e.target,"a")}},beforeunload:{postDispatch:function(e){e.result!==t&&(e.originalEvent.returnValue=e.result)}}},simulate:function(e,t,n,r){var i=x.extend(new x.Event,n,{type:e,isSimulated:!0,originalEvent:{}});r?x.event.trigger(i,null,t):x.event.dispatch.call(t,i),i.isDefaultPrevented()&&n.preventDefault()}},x.removeEvent=a.removeEventListener?function(e,t,n){e.removeEventListener&&e.removeEventListener(t,n,!1)}:function(e,t,n){var r="on"+t;e.detachEvent&&(typeof e[r]===i&&(e[r]=null),e.detachEvent(r,n))},x.Event=function(e,n){return this instanceof x.Event?(e&&e.type?(this.originalEvent=e,this.type=e.type,this.isDefaultPrevented=e.defaultPrevented||e.returnValue===!1||e.getPreventDefault&&e.getPreventDefault()?it:ot):this.type=e,n&&x.extend(this,n),this.timeStamp=e&&e.timeStamp||x.now(),this[x.expando]=!0,t):new x.Event(e,n)},x.Event.prototype={isDefaultPrevented:ot,isPropagationStopped:ot,isImmediatePropagationStopped:ot,preventDefault:function(){var e=this.originalEvent;this.isDefaultPrevented=it,e&&(e.preventDefault?e.preventDefault():e.returnValue=!1)},stopPropagation:function(){var e=this.originalEvent;this.isPropagationStopped=it,e&&(e.stopPropagation&&e.stopPropagation(),e.cancelBubble=!0)},stopImmediatePropagation:function(){this.isImmediatePropagationStopped=it,this.stopPropagation()}},x.each({mouseenter:"mouseover",mouseleave:"mouseout"},function(e,t){x.event.special[e]={delegateType:t,bindType:t,handle:function(e){var n,r=this,i=e.relatedTarget,o=e.handleObj;return(!i||i!==r&&!x.contains(r,i))&&(e.type=o.origType,n=o.handler.apply(this,arguments),e.type=t),n}}}),x.support.submitBubbles||(x.event.special.submit={setup:function(){return x.nodeName(this,"form")?!1:(x.event.add(this,"click._submit keypress._submit",function(e){var n=e.target,r=x.nodeName(n,"input")||x.nodeName(n,"button")?n.form:t;r&&!x._data(r,"submitBubbles")&&(x.event.add(r,"submit._submit",function(e){e._submit_bubble=!0}),x._data(r,"submitBubbles",!0))}),t)},postDispatch:function(e){e._submit_bubble&&(delete e._submit_bubble,this.parentNode&&!e.isTrigger&&x.event.simulate("submit",this.parentNode,e,!0))},teardown:function(){return x.nodeName(this,"form")?!1:(x.event.remove(this,"._submit"),t)}}),x.support.changeBubbles||(x.event.special.change={setup:function(){return Z.test(this.nodeName)?(("checkbox"===this.type||"radio"===this.type)&&(x.event.add(this,"propertychange._change",function(e){"checked"===e.originalEvent.propertyName&&(this._just_changed=!0)}),x.event.add(this,"click._change",function(e){this._just_changed&&!e.isTrigger&&(this._just_changed=!1),x.event.simulate("change",this,e,!0)})),!1):(x.event.add(this,"beforeactivate._change",function(e){var t=e.target;Z.test(t.nodeName)&&!x._data(t,"changeBubbles")&&(x.event.add(t,"change._change",function(e){!this.parentNode||e.isSimulated||e.isTrigger||x.event.simulate("change",this.parentNode,e,!0)}),x._data(t,"changeBubbles",!0))}),t)},handle:function(e){var n=e.target;return this!==n||e.isSimulated||e.isTrigger||"radio"!==n.type&&"checkbox"!==n.type?e.handleObj.handler.apply(this,arguments):t},teardown:function(){return x.event.remove(this,"._change"),!Z.test(this.nodeName)}}),x.support.focusinBubbles||x.each({focus:"focusin",blur:"focusout"},function(e,t){var n=0,r=function(e){x.event.simulate(t,e.target,x.event.fix(e),!0)};x.event.special[t]={setup:function(){0===n++&&a.addEventListener(e,r,!0)},teardown:function(){0===--n&&a.removeEventListener(e,r,!0)}}}),x.fn.extend({on:function(e,n,r,i,o){var a,s;if("object"==typeof e){"string"!=typeof n&&(r=r||n,n=t);for(a in e)this.on(a,n,r,e[a],o);return this}if(null==r&&null==i?(i=n,r=n=t):null==i&&("string"==typeof n?(i=r,r=t):(i=r,r=n,n=t)),i===!1)i=ot;else if(!i)return this;return 1===o&&(s=i,i=function(e){return x().off(e),s.apply(this,arguments)},i.guid=s.guid||(s.guid=x.guid++)),this.each(function(){x.event.add(this,e,i,r,n)})},one:function(e,t,n,r){return this.on(e,t,n,r,1)},off:function(e,n,r){var i,o;if(e&&e.preventDefault&&e.handleObj)return i=e.handleObj,x(e.delegateTarget).off(i.namespace?i.origType+"."+i.namespace:i.origType,i.selector,i.handler),this;if("object"==typeof e){for(o in e)this.off(o,n,e[o]);return this}return(n===!1||"function"==typeof n)&&(r=n,n=t),r===!1&&(r=ot),this.each(function(){x.event.remove(this,e,r,n)})},trigger:function(e,t){return this.each(function(){x.event.trigger(e,t,this)})},triggerHandler:function(e,n){var r=this[0];return r?x.event.trigger(e,n,r,!0):t}});var st=/^.[^:#\[\.,]*$/,lt=/^(?:parents|prev(?:Until|All))/,ut=x.expr.match.needsContext,ct={children:!0,contents:!0,next:!0,prev:!0};x.fn.extend({find:function(e){var t,n=[],r=this,i=r.length;if("string"!=typeof e)return this.pushStack(x(e).filter(function(){for(t=0;i>t;t++)if(x.contains(r[t],this))return!0}));for(t=0;i>t;t++)x.find(e,r[t],n);return n=this.pushStack(i>1?x.unique(n):n),n.selector=this.selector?this.selector+" "+e:e,n},has:function(e){var t,n=x(e,this),r=n.length;return this.filter(function(){for(t=0;r>t;t++)if(x.contains(this,n[t]))return!0})},not:function(e){return this.pushStack(ft(this,e||[],!0))},filter:function(e){return this.pushStack(ft(this,e||[],!1))},is:function(e){return!!ft(this,"string"==typeof e&&ut.test(e)?x(e):e||[],!1).length},closest:function(e,t){var n,r=0,i=this.length,o=[],a=ut.test(e)||"string"!=typeof e?x(e,t||this.context):0;for(;i>r;r++)for(n=this[r];n&&n!==t;n=n.parentNode)if(11>n.nodeType&&(a?a.index(n)>-1:1===n.nodeType&&x.find.matchesSelector(n,e))){n=o.push(n);break}return this.pushStack(o.length>1?x.unique(o):o)},index:function(e){return e?"string"==typeof e?x.inArray(this[0],x(e)):x.inArray(e.jquery?e[0]:e,this):this[0]&&this[0].parentNode?this.first().prevAll().length:-1},add:function(e,t){var n="string"==typeof e?x(e,t):x.makeArray(e&&e.nodeType?[e]:e),r=x.merge(this.get(),n);return this.pushStack(x.unique(r))},addBack:function(e){return this.add(null==e?this.prevObject:this.prevObject.filter(e))}});function pt(e,t){do e=e[t];while(e&&1!==e.nodeType);return e}x.each({parent:function(e){var t=e.parentNode;return t&&11!==t.nodeType?t:null},parents:function(e){return x.dir(e,"parentNode")},parentsUntil:function(e,t,n){return x.dir(e,"parentNode",n)},next:function(e){return pt(e,"nextSibling")},prev:function(e){return pt(e,"previousSibling")},nextAll:function(e){return x.dir(e,"nextSibling")},prevAll:function(e){return x.dir(e,"previousSibling")},nextUntil:function(e,t,n){return x.dir(e,"nextSibling",n)},prevUntil:function(e,t,n){return x.dir(e,"previousSibling",n)},siblings:function(e){return x.sibling((e.parentNode||{}).firstChild,e)},children:function(e){return x.sibling(e.firstChild)},contents:function(e){return x.nodeName(e,"iframe")?e.contentDocument||e.contentWindow.document:x.merge([],e.childNodes)}},function(e,t){x.fn[e]=function(n,r){var i=x.map(this,t,n);return"Until"!==e.slice(-5)&&(r=n),r&&"string"==typeof r&&(i=x.filter(r,i)),this.length>1&&(ct[e]||(i=x.unique(i)),lt.test(e)&&(i=i.reverse())),this.pushStack(i)}}),x.extend({filter:function(e,t,n){var r=t[0];return n&&(e=":not("+e+")"),1===t.length&&1===r.nodeType?x.find.matchesSelector(r,e)?[r]:[]:x.find.matches(e,x.grep(t,function(e){return 1===e.nodeType}))},dir:function(e,n,r){var i=[],o=e[n];while(o&&9!==o.nodeType&&(r===t||1!==o.nodeType||!x(o).is(r)))1===o.nodeType&&i.push(o),o=o[n];return i},sibling:function(e,t){var n=[];for(;e;e=e.nextSibling)1===e.nodeType&&e!==t&&n.push(e);return n}});function ft(e,t,n){if(x.isFunction(t))return x.grep(e,function(e,r){return!!t.call(e,r,e)!==n});if(t.nodeType)return x.grep(e,function(e){return e===t!==n});if("string"==typeof t){if(st.test(t))return x.filter(t,e,n);t=x.filter(t,e)}return x.grep(e,function(e){return x.inArray(e,t)>=0!==n})}function dt(e){var t=ht.split("|"),n=e.createDocumentFragment();if(n.createElement)while(t.length)n.createElement(t.pop());return n}var ht="abbr|article|aside|audio|bdi|canvas|data|datalist|details|figcaption|figure|footer|header|hgroup|mark|meter|nav|output|progress|section|summary|time|video",gt=/ jQuery\d+="(?:null|\d+)"/g,mt=RegExp("<(?:"+ht+")[\\s/>]","i"),yt=/^\s+/,vt=/<(?!area|br|col|embed|hr|img|input|link|meta|param)(([\w:]+)[^>]*)\/>/gi,bt=/<([\w:]+)/,xt=/<tbody/i,wt=/<|&#?\w+;/,Tt=/<(?:script|style|link)/i,Ct=/^(?:checkbox|radio)$/i,Nt=/checked\s*(?:[^=]|=\s*.checked.)/i,kt=/^$|\/(?:java|ecma)script/i,Et=/^true\/(.*)/,St=/^\s*<!(?:\[CDATA\[|--)|(?:\]\]|--)>\s*$/g,At={option:[1,"<select multiple='multiple'>","</select>"],legend:[1,"<fieldset>","</fieldset>"],area:[1,"<map>","</map>"],param:[1,"<object>","</object>"],thead:[1,"<table>","</table>"],tr:[2,"<table><tbody>","</tbody></table>"],col:[2,"<table><tbody></tbody><colgroup>","</colgroup></table>"],td:[3,"<table><tbody><tr>","</tr></tbody></table>"],_default:x.support.htmlSerialize?[0,"",""]:[1,"X<div>","</div>"]},jt=dt(a),Dt=jt.appendChild(a.createElement("div"));At.optgroup=At.option,At.tbody=At.tfoot=At.colgroup=At.caption=At.thead,At.th=At.td,x.fn.extend({text:function(e){return x.access(this,function(e){return e===t?x.text(this):this.empty().append((this[0]&&this[0].ownerDocument||a).createTextNode(e))},null,e,arguments.length)},append:function(){return this.domManip(arguments,function(e){if(1===this.nodeType||11===this.nodeType||9===this.nodeType){var t=Lt(this,e);t.appendChild(e)}})},prepend:function(){return this.domManip(arguments,function(e){if(1===this.nodeType||11===this.nodeType||9===this.nodeType){var t=Lt(this,e);t.insertBefore(e,t.firstChild)}})},before:function(){return this.domManip(arguments,function(e){this.parentNode&&this.parentNode.insertBefore(e,this)})},after:function(){return this.domManip(arguments,function(e){this.parentNode&&this.parentNode.insertBefore(e,this.nextSibling)})},remove:function(e,t){var n,r=e?x.filter(e,this):this,i=0;for(;null!=(n=r[i]);i++)t||1!==n.nodeType||x.cleanData(Ft(n)),n.parentNode&&(t&&x.contains(n.ownerDocument,n)&&_t(Ft(n,"script")),n.parentNode.removeChild(n));return this},empty:function(){var e,t=0;for(;null!=(e=this[t]);t++){1===e.nodeType&&x.cleanData(Ft(e,!1));while(e.firstChild)e.removeChild(e.firstChild);e.options&&x.nodeName(e,"select")&&(e.options.length=0)}return this},clone:function(e,t){return e=null==e?!1:e,t=null==t?e:t,this.map(function(){return x.clone(this,e,t)})},html:function(e){return x.access(this,function(e){var n=this[0]||{},r=0,i=this.length;if(e===t)return 1===n.nodeType?n.innerHTML.replace(gt,""):t;if(!("string"!=typeof e||Tt.test(e)||!x.support.htmlSerialize&&mt.test(e)||!x.support.leadingWhitespace&&yt.test(e)||At[(bt.exec(e)||["",""])[1].toLowerCase()])){e=e.replace(vt,"<$1></$2>");try{for(;i>r;r++)n=this[r]||{},1===n.nodeType&&(x.cleanData(Ft(n,!1)),n.innerHTML=e);n=0}catch(o){}}n&&this.empty().append(e)},null,e,arguments.length)},replaceWith:function(){var e=x.map(this,function(e){return[e.nextSibling,e.parentNode]}),t=0;return this.domManip(arguments,function(n){var r=e[t++],i=e[t++];i&&(r&&r.parentNode!==i&&(r=this.nextSibling),x(this).remove(),i.insertBefore(n,r))},!0),t?this:this.remove()},detach:function(e){return this.remove(e,!0)},domManip:function(e,t,n){e=d.apply([],e);var r,i,o,a,s,l,u=0,c=this.length,p=this,f=c-1,h=e[0],g=x.isFunction(h);if(g||!(1>=c||"string"!=typeof h||x.support.checkClone)&&Nt.test(h))return this.each(function(r){var i=p.eq(r);g&&(e[0]=h.call(this,r,i.html())),i.domManip(e,t,n)});if(c&&(l=x.buildFragment(e,this[0].ownerDocument,!1,!n&&this),r=l.firstChild,1===l.childNodes.length&&(l=r),r)){for(a=x.map(Ft(l,"script"),Ht),o=a.length;c>u;u++)i=l,u!==f&&(i=x.clone(i,!0,!0),o&&x.merge(a,Ft(i,"script"))),t.call(this[u],i,u);if(o)for(s=a[a.length-1].ownerDocument,x.map(a,qt),u=0;o>u;u++)i=a[u],kt.test(i.type||"")&&!x._data(i,"globalEval")&&x.contains(s,i)&&(i.src?x._evalUrl(i.src):x.globalEval((i.text||i.textContent||i.innerHTML||"").replace(St,"")));l=r=null}return this}});function Lt(e,t){return x.nodeName(e,"table")&&x.nodeName(1===t.nodeType?t:t.firstChild,"tr")?e.getElementsByTagName("tbody")[0]||e.appendChild(e.ownerDocument.createElement("tbody")):e}function Ht(e){return e.type=(null!==x.find.attr(e,"type"))+"/"+e.type,e}function qt(e){var t=Et.exec(e.type);return t?e.type=t[1]:e.removeAttribute("type"),e}function _t(e,t){var n,r=0;for(;null!=(n=e[r]);r++)x._data(n,"globalEval",!t||x._data(t[r],"globalEval"))}function Mt(e,t){if(1===t.nodeType&&x.hasData(e)){var n,r,i,o=x._data(e),a=x._data(t,o),s=o.events;if(s){delete a.handle,a.events={};for(n in s)for(r=0,i=s[n].length;i>r;r++)x.event.add(t,n,s[n][r])}a.data&&(a.data=x.extend({},a.data))}}function Ot(e,t){var n,r,i;if(1===t.nodeType){if(n=t.nodeName.toLowerCase(),!x.support.noCloneEvent&&t[x.expando]){i=x._data(t);for(r in i.events)x.removeEvent(t,r,i.handle);t.removeAttribute(x.expando)}"script"===n&&t.text!==e.text?(Ht(t).text=e.text,qt(t)):"object"===n?(t.parentNode&&(t.outerHTML=e.outerHTML),x.support.html5Clone&&e.innerHTML&&!x.trim(t.innerHTML)&&(t.innerHTML=e.innerHTML)):"input"===n&&Ct.test(e.type)?(t.defaultChecked=t.checked=e.checked,t.value!==e.value&&(t.value=e.value)):"option"===n?t.defaultSelected=t.selected=e.defaultSelected:("input"===n||"textarea"===n)&&(t.defaultValue=e.defaultValue)}}x.each({appendTo:"append",prependTo:"prepend",insertBefore:"before",insertAfter:"after",replaceAll:"replaceWith"},function(e,t){x.fn[e]=function(e){var n,r=0,i=[],o=x(e),a=o.length-1;for(;a>=r;r++)n=r===a?this:this.clone(!0),x(o[r])[t](n),h.apply(i,n.get());return this.pushStack(i)}});function Ft(e,n){var r,o,a=0,s=typeof e.getElementsByTagName!==i?e.getElementsByTagName(n||"*"):typeof e.querySelectorAll!==i?e.querySelectorAll(n||"*"):t;if(!s)for(s=[],r=e.childNodes||e;null!=(o=r[a]);a++)!n||x.nodeName(o,n)?s.push(o):x.merge(s,Ft(o,n));return n===t||n&&x.nodeName(e,n)?x.merge([e],s):s}function Bt(e){Ct.test(e.type)&&(e.defaultChecked=e.checked)}x.extend({clone:function(e,t,n){var r,i,o,a,s,l=x.contains(e.ownerDocument,e);if(x.support.html5Clone||x.isXMLDoc(e)||!mt.test("<"+e.nodeName+">")?o=e.cloneNode(!0):(Dt.innerHTML=e.outerHTML,Dt.removeChild(o=Dt.firstChild)),!(x.support.noCloneEvent&&x.support.noCloneChecked||1!==e.nodeType&&11!==e.nodeType||x.isXMLDoc(e)))for(r=Ft(o),s=Ft(e),a=0;null!=(i=s[a]);++a)r[a]&&Ot(i,r[a]);if(t)if(n)for(s=s||Ft(e),r=r||Ft(o),a=0;null!=(i=s[a]);a++)Mt(i,r[a]);else Mt(e,o);return r=Ft(o,"script"),r.length>0&&_t(r,!l&&Ft(e,"script")),r=s=i=null,o},buildFragment:function(e,t,n,r){var i,o,a,s,l,u,c,p=e.length,f=dt(t),d=[],h=0;for(;p>h;h++)if(o=e[h],o||0===o)if("object"===x.type(o))x.merge(d,o.nodeType?[o]:o);else if(wt.test(o)){s=s||f.appendChild(t.createElement("div")),l=(bt.exec(o)||["",""])[1].toLowerCase(),c=At[l]||At._default,s.innerHTML=c[1]+o.replace(vt,"<$1></$2>")+c[2],i=c[0];while(i--)s=s.lastChild;if(!x.support.leadingWhitespace&&yt.test(o)&&d.push(t.createTextNode(yt.exec(o)[0])),!x.support.tbody){o="table"!==l||xt.test(o)?"<table>"!==c[1]||xt.test(o)?0:s:s.firstChild,i=o&&o.childNodes.length;while(i--)x.nodeName(u=o.childNodes[i],"tbody")&&!u.childNodes.length&&o.removeChild(u)}x.merge(d,s.childNodes),s.textContent="";while(s.firstChild)s.removeChild(s.firstChild);s=f.lastChild}else d.push(t.createTextNode(o));s&&f.removeChild(s),x.support.appendChecked||x.grep(Ft(d,"input"),Bt),h=0;while(o=d[h++])if((!r||-1===x.inArray(o,r))&&(a=x.contains(o.ownerDocument,o),s=Ft(f.appendChild(o),"script"),a&&_t(s),n)){i=0;while(o=s[i++])kt.test(o.type||"")&&n.push(o)}return s=null,f},cleanData:function(e,t){var n,r,o,a,s=0,l=x.expando,u=x.cache,c=x.support.deleteExpando,f=x.event.special;for(;null!=(n=e[s]);s++)if((t||x.acceptData(n))&&(o=n[l],a=o&&u[o])){if(a.events)for(r in a.events)f[r]?x.event.remove(n,r):x.removeEvent(n,r,a.handle);
u[o]&&(delete u[o],c?delete n[l]:typeof n.removeAttribute!==i?n.removeAttribute(l):n[l]=null,p.push(o))}},_evalUrl:function(e){return x.ajax({url:e,type:"GET",dataType:"script",async:!1,global:!1,"throws":!0})}}),x.fn.extend({wrapAll:function(e){if(x.isFunction(e))return this.each(function(t){x(this).wrapAll(e.call(this,t))});if(this[0]){var t=x(e,this[0].ownerDocument).eq(0).clone(!0);this[0].parentNode&&t.insertBefore(this[0]),t.map(function(){var e=this;while(e.firstChild&&1===e.firstChild.nodeType)e=e.firstChild;return e}).append(this)}return this},wrapInner:function(e){return x.isFunction(e)?this.each(function(t){x(this).wrapInner(e.call(this,t))}):this.each(function(){var t=x(this),n=t.contents();n.length?n.wrapAll(e):t.append(e)})},wrap:function(e){var t=x.isFunction(e);return this.each(function(n){x(this).wrapAll(t?e.call(this,n):e)})},unwrap:function(){return this.parent().each(function(){x.nodeName(this,"body")||x(this).replaceWith(this.childNodes)}).end()}});var Pt,Rt,Wt,$t=/alpha\([^)]*\)/i,It=/opacity\s*=\s*([^)]*)/,zt=/^(top|right|bottom|left)$/,Xt=/^(none|table(?!-c[ea]).+)/,Ut=/^margin/,Vt=RegExp("^("+w+")(.*)$","i"),Yt=RegExp("^("+w+")(?!px)[a-z%]+$","i"),Jt=RegExp("^([+-])=("+w+")","i"),Gt={BODY:"block"},Qt={position:"absolute",visibility:"hidden",display:"block"},Kt={letterSpacing:0,fontWeight:400},Zt=["Top","Right","Bottom","Left"],en=["Webkit","O","Moz","ms"];function tn(e,t){if(t in e)return t;var n=t.charAt(0).toUpperCase()+t.slice(1),r=t,i=en.length;while(i--)if(t=en[i]+n,t in e)return t;return r}function nn(e,t){return e=t||e,"none"===x.css(e,"display")||!x.contains(e.ownerDocument,e)}function rn(e,t){var n,r,i,o=[],a=0,s=e.length;for(;s>a;a++)r=e[a],r.style&&(o[a]=x._data(r,"olddisplay"),n=r.style.display,t?(o[a]||"none"!==n||(r.style.display=""),""===r.style.display&&nn(r)&&(o[a]=x._data(r,"olddisplay",ln(r.nodeName)))):o[a]||(i=nn(r),(n&&"none"!==n||!i)&&x._data(r,"olddisplay",i?n:x.css(r,"display"))));for(a=0;s>a;a++)r=e[a],r.style&&(t&&"none"!==r.style.display&&""!==r.style.display||(r.style.display=t?o[a]||"":"none"));return e}x.fn.extend({css:function(e,n){return x.access(this,function(e,n,r){var i,o,a={},s=0;if(x.isArray(n)){for(o=Rt(e),i=n.length;i>s;s++)a[n[s]]=x.css(e,n[s],!1,o);return a}return r!==t?x.style(e,n,r):x.css(e,n)},e,n,arguments.length>1)},show:function(){return rn(this,!0)},hide:function(){return rn(this)},toggle:function(e){return"boolean"==typeof e?e?this.show():this.hide():this.each(function(){nn(this)?x(this).show():x(this).hide()})}}),x.extend({cssHooks:{opacity:{get:function(e,t){if(t){var n=Wt(e,"opacity");return""===n?"1":n}}}},cssNumber:{columnCount:!0,fillOpacity:!0,fontWeight:!0,lineHeight:!0,opacity:!0,order:!0,orphans:!0,widows:!0,zIndex:!0,zoom:!0},cssProps:{"float":x.support.cssFloat?"cssFloat":"styleFloat"},style:function(e,n,r,i){if(e&&3!==e.nodeType&&8!==e.nodeType&&e.style){var o,a,s,l=x.camelCase(n),u=e.style;if(n=x.cssProps[l]||(x.cssProps[l]=tn(u,l)),s=x.cssHooks[n]||x.cssHooks[l],r===t)return s&&"get"in s&&(o=s.get(e,!1,i))!==t?o:u[n];if(a=typeof r,"string"===a&&(o=Jt.exec(r))&&(r=(o[1]+1)*o[2]+parseFloat(x.css(e,n)),a="number"),!(null==r||"number"===a&&isNaN(r)||("number"!==a||x.cssNumber[l]||(r+="px"),x.support.clearCloneStyle||""!==r||0!==n.indexOf("background")||(u[n]="inherit"),s&&"set"in s&&(r=s.set(e,r,i))===t)))try{u[n]=r}catch(c){}}},css:function(e,n,r,i){var o,a,s,l=x.camelCase(n);return n=x.cssProps[l]||(x.cssProps[l]=tn(e.style,l)),s=x.cssHooks[n]||x.cssHooks[l],s&&"get"in s&&(a=s.get(e,!0,r)),a===t&&(a=Wt(e,n,i)),"normal"===a&&n in Kt&&(a=Kt[n]),""===r||r?(o=parseFloat(a),r===!0||x.isNumeric(o)?o||0:a):a}}),e.getComputedStyle?(Rt=function(t){return e.getComputedStyle(t,null)},Wt=function(e,n,r){var i,o,a,s=r||Rt(e),l=s?s.getPropertyValue(n)||s[n]:t,u=e.style;return s&&(""!==l||x.contains(e.ownerDocument,e)||(l=x.style(e,n)),Yt.test(l)&&Ut.test(n)&&(i=u.width,o=u.minWidth,a=u.maxWidth,u.minWidth=u.maxWidth=u.width=l,l=s.width,u.width=i,u.minWidth=o,u.maxWidth=a)),l}):a.documentElement.currentStyle&&(Rt=function(e){return e.currentStyle},Wt=function(e,n,r){var i,o,a,s=r||Rt(e),l=s?s[n]:t,u=e.style;return null==l&&u&&u[n]&&(l=u[n]),Yt.test(l)&&!zt.test(n)&&(i=u.left,o=e.runtimeStyle,a=o&&o.left,a&&(o.left=e.currentStyle.left),u.left="fontSize"===n?"1em":l,l=u.pixelLeft+"px",u.left=i,a&&(o.left=a)),""===l?"auto":l});function on(e,t,n){var r=Vt.exec(t);return r?Math.max(0,r[1]-(n||0))+(r[2]||"px"):t}function an(e,t,n,r,i){var o=n===(r?"border":"content")?4:"width"===t?1:0,a=0;for(;4>o;o+=2)"margin"===n&&(a+=x.css(e,n+Zt[o],!0,i)),r?("content"===n&&(a-=x.css(e,"padding"+Zt[o],!0,i)),"margin"!==n&&(a-=x.css(e,"border"+Zt[o]+"Width",!0,i))):(a+=x.css(e,"padding"+Zt[o],!0,i),"padding"!==n&&(a+=x.css(e,"border"+Zt[o]+"Width",!0,i)));return a}function sn(e,t,n){var r=!0,i="width"===t?e.offsetWidth:e.offsetHeight,o=Rt(e),a=x.support.boxSizing&&"border-box"===x.css(e,"boxSizing",!1,o);if(0>=i||null==i){if(i=Wt(e,t,o),(0>i||null==i)&&(i=e.style[t]),Yt.test(i))return i;r=a&&(x.support.boxSizingReliable||i===e.style[t]),i=parseFloat(i)||0}return i+an(e,t,n||(a?"border":"content"),r,o)+"px"}function ln(e){var t=a,n=Gt[e];return n||(n=un(e,t),"none"!==n&&n||(Pt=(Pt||x("<iframe frameborder='0' width='0' height='0'/>").css("cssText","display:block !important")).appendTo(t.documentElement),t=(Pt[0].contentWindow||Pt[0].contentDocument).document,t.write("<!doctype html><html><body>"),t.close(),n=un(e,t),Pt.detach()),Gt[e]=n),n}function un(e,t){var n=x(t.createElement(e)).appendTo(t.body),r=x.css(n[0],"display");return n.remove(),r}x.each(["height","width"],function(e,n){x.cssHooks[n]={get:function(e,r,i){return r?0===e.offsetWidth&&Xt.test(x.css(e,"display"))?x.swap(e,Qt,function(){return sn(e,n,i)}):sn(e,n,i):t},set:function(e,t,r){var i=r&&Rt(e);return on(e,t,r?an(e,n,r,x.support.boxSizing&&"border-box"===x.css(e,"boxSizing",!1,i),i):0)}}}),x.support.opacity||(x.cssHooks.opacity={get:function(e,t){return It.test((t&&e.currentStyle?e.currentStyle.filter:e.style.filter)||"")?.01*parseFloat(RegExp.$1)+"":t?"1":""},set:function(e,t){var n=e.style,r=e.currentStyle,i=x.isNumeric(t)?"alpha(opacity="+100*t+")":"",o=r&&r.filter||n.filter||"";n.zoom=1,(t>=1||""===t)&&""===x.trim(o.replace($t,""))&&n.removeAttribute&&(n.removeAttribute("filter"),""===t||r&&!r.filter)||(n.filter=$t.test(o)?o.replace($t,i):o+" "+i)}}),x(function(){x.support.reliableMarginRight||(x.cssHooks.marginRight={get:function(e,n){return n?x.swap(e,{display:"inline-block"},Wt,[e,"marginRight"]):t}}),!x.support.pixelPosition&&x.fn.position&&x.each(["top","left"],function(e,n){x.cssHooks[n]={get:function(e,r){return r?(r=Wt(e,n),Yt.test(r)?x(e).position()[n]+"px":r):t}}})}),x.expr&&x.expr.filters&&(x.expr.filters.hidden=function(e){return 0>=e.offsetWidth&&0>=e.offsetHeight||!x.support.reliableHiddenOffsets&&"none"===(e.style&&e.style.display||x.css(e,"display"))},x.expr.filters.visible=function(e){return!x.expr.filters.hidden(e)}),x.each({margin:"",padding:"",border:"Width"},function(e,t){x.cssHooks[e+t]={expand:function(n){var r=0,i={},o="string"==typeof n?n.split(" "):[n];for(;4>r;r++)i[e+Zt[r]+t]=o[r]||o[r-2]||o[0];return i}},Ut.test(e)||(x.cssHooks[e+t].set=on)});var cn=/%20/g,pn=/\[\]$/,fn=/\r?\n/g,dn=/^(?:submit|button|image|reset|file)$/i,hn=/^(?:input|select|textarea|keygen)/i;x.fn.extend({serialize:function(){return x.param(this.serializeArray())},serializeArray:function(){return this.map(function(){var e=x.prop(this,"elements");return e?x.makeArray(e):this}).filter(function(){var e=this.type;return this.name&&!x(this).is(":disabled")&&hn.test(this.nodeName)&&!dn.test(e)&&(this.checked||!Ct.test(e))}).map(function(e,t){var n=x(this).val();return null==n?null:x.isArray(n)?x.map(n,function(e){return{name:t.name,value:e.replace(fn,"\r\n")}}):{name:t.name,value:n.replace(fn,"\r\n")}}).get()}}),x.param=function(e,n){var r,i=[],o=function(e,t){t=x.isFunction(t)?t():null==t?"":t,i[i.length]=encodeURIComponent(e)+"="+encodeURIComponent(t)};if(n===t&&(n=x.ajaxSettings&&x.ajaxSettings.traditional),x.isArray(e)||e.jquery&&!x.isPlainObject(e))x.each(e,function(){o(this.name,this.value)});else for(r in e)gn(r,e[r],n,o);return i.join("&").replace(cn,"+")};function gn(e,t,n,r){var i;if(x.isArray(t))x.each(t,function(t,i){n||pn.test(e)?r(e,i):gn(e+"["+("object"==typeof i?t:"")+"]",i,n,r)});else if(n||"object"!==x.type(t))r(e,t);else for(i in t)gn(e+"["+i+"]",t[i],n,r)}x.each("blur focus focusin focusout load resize scroll unload click dblclick mousedown mouseup mousemove mouseover mouseout mouseenter mouseleave change select submit keydown keypress keyup error contextmenu".split(" "),function(e,t){x.fn[t]=function(e,n){return arguments.length>0?this.on(t,null,e,n):this.trigger(t)}}),x.fn.extend({hover:function(e,t){return this.mouseenter(e).mouseleave(t||e)},bind:function(e,t,n){return this.on(e,null,t,n)},unbind:function(e,t){return this.off(e,null,t)},delegate:function(e,t,n,r){return this.on(t,e,n,r)},undelegate:function(e,t,n){return 1===arguments.length?this.off(e,"**"):this.off(t,e||"**",n)}});var mn,yn,vn=x.now(),bn=/\?/,xn=/#.*$/,wn=/([?&])_=[^&]*/,Tn=/^(.*?):[ \t]*([^\r\n]*)\r?$/gm,Cn=/^(?:about|app|app-storage|.+-extension|file|res|widget):$/,Nn=/^(?:GET|HEAD)$/,kn=/^\/\//,En=/^([\w.+-]+:)(?:\/\/([^\/?#:]*)(?::(\d+)|)|)/,Sn=x.fn.load,An={},jn={},Dn="*/".concat("*");try{yn=o.href}catch(Ln){yn=a.createElement("a"),yn.href="",yn=yn.href}mn=En.exec(yn.toLowerCase())||[];function Hn(e){return function(t,n){"string"!=typeof t&&(n=t,t="*");var r,i=0,o=t.toLowerCase().match(T)||[];if(x.isFunction(n))while(r=o[i++])"+"===r[0]?(r=r.slice(1)||"*",(e[r]=e[r]||[]).unshift(n)):(e[r]=e[r]||[]).push(n)}}function qn(e,n,r,i){var o={},a=e===jn;function s(l){var u;return o[l]=!0,x.each(e[l]||[],function(e,l){var c=l(n,r,i);return"string"!=typeof c||a||o[c]?a?!(u=c):t:(n.dataTypes.unshift(c),s(c),!1)}),u}return s(n.dataTypes[0])||!o["*"]&&s("*")}function _n(e,n){var r,i,o=x.ajaxSettings.flatOptions||{};for(i in n)n[i]!==t&&((o[i]?e:r||(r={}))[i]=n[i]);return r&&x.extend(!0,e,r),e}x.fn.load=function(e,n,r){if("string"!=typeof e&&Sn)return Sn.apply(this,arguments);var i,o,a,s=this,l=e.indexOf(" ");return l>=0&&(i=e.slice(l,e.length),e=e.slice(0,l)),x.isFunction(n)?(r=n,n=t):n&&"object"==typeof n&&(a="POST"),s.length>0&&x.ajax({url:e,type:a,dataType:"html",data:n}).done(function(e){o=arguments,s.html(i?x("<div>").append(x.parseHTML(e)).find(i):e)}).complete(r&&function(e,t){s.each(r,o||[e.responseText,t,e])}),this},x.each(["ajaxStart","ajaxStop","ajaxComplete","ajaxError","ajaxSuccess","ajaxSend"],function(e,t){x.fn[t]=function(e){return this.on(t,e)}}),x.extend({active:0,lastModified:{},etag:{},ajaxSettings:{url:yn,type:"GET",isLocal:Cn.test(mn[1]),global:!0,processData:!0,async:!0,contentType:"application/x-www-form-urlencoded; charset=UTF-8",accepts:{"*":Dn,text:"text/plain",html:"text/html",xml:"application/xml, text/xml",json:"application/json, text/javascript"},contents:{xml:/xml/,html:/html/,json:/json/},responseFields:{xml:"responseXML",text:"responseText",json:"responseJSON"},converters:{"* text":String,"text html":!0,"text json":x.parseJSON,"text xml":x.parseXML},flatOptions:{url:!0,context:!0}},ajaxSetup:function(e,t){return t?_n(_n(e,x.ajaxSettings),t):_n(x.ajaxSettings,e)},ajaxPrefilter:Hn(An),ajaxTransport:Hn(jn),ajax:function(e,n){"object"==typeof e&&(n=e,e=t),n=n||{};var r,i,o,a,s,l,u,c,p=x.ajaxSetup({},n),f=p.context||p,d=p.context&&(f.nodeType||f.jquery)?x(f):x.event,h=x.Deferred(),g=x.Callbacks("once memory"),m=p.statusCode||{},y={},v={},b=0,w="canceled",C={readyState:0,getResponseHeader:function(e){var t;if(2===b){if(!c){c={};while(t=Tn.exec(a))c[t[1].toLowerCase()]=t[2]}t=c[e.toLowerCase()]}return null==t?null:t},getAllResponseHeaders:function(){return 2===b?a:null},setRequestHeader:function(e,t){var n=e.toLowerCase();return b||(e=v[n]=v[n]||e,y[e]=t),this},overrideMimeType:function(e){return b||(p.mimeType=e),this},statusCode:function(e){var t;if(e)if(2>b)for(t in e)m[t]=[m[t],e[t]];else C.always(e[C.status]);return this},abort:function(e){var t=e||w;return u&&u.abort(t),k(0,t),this}};if(h.promise(C).complete=g.add,C.success=C.done,C.error=C.fail,p.url=((e||p.url||yn)+"").replace(xn,"").replace(kn,mn[1]+"//"),p.type=n.method||n.type||p.method||p.type,p.dataTypes=x.trim(p.dataType||"*").toLowerCase().match(T)||[""],null==p.crossDomain&&(r=En.exec(p.url.toLowerCase()),p.crossDomain=!(!r||r[1]===mn[1]&&r[2]===mn[2]&&(r[3]||("http:"===r[1]?"80":"443"))===(mn[3]||("http:"===mn[1]?"80":"443")))),p.data&&p.processData&&"string"!=typeof p.data&&(p.data=x.param(p.data,p.traditional)),qn(An,p,n,C),2===b)return C;l=p.global,l&&0===x.active++&&x.event.trigger("ajaxStart"),p.type=p.type.toUpperCase(),p.hasContent=!Nn.test(p.type),o=p.url,p.hasContent||(p.data&&(o=p.url+=(bn.test(o)?"&":"?")+p.data,delete p.data),p.cache===!1&&(p.url=wn.test(o)?o.replace(wn,"$1_="+vn++):o+(bn.test(o)?"&":"?")+"_="+vn++)),p.ifModified&&(x.lastModified[o]&&C.setRequestHeader("If-Modified-Since",x.lastModified[o]),x.etag[o]&&C.setRequestHeader("If-None-Match",x.etag[o])),(p.data&&p.hasContent&&p.contentType!==!1||n.contentType)&&C.setRequestHeader("Content-Type",p.contentType),C.setRequestHeader("Accept",p.dataTypes[0]&&p.accepts[p.dataTypes[0]]?p.accepts[p.dataTypes[0]]+("*"!==p.dataTypes[0]?", "+Dn+"; q=0.01":""):p.accepts["*"]);for(i in p.headers)C.setRequestHeader(i,p.headers[i]);if(p.beforeSend&&(p.beforeSend.call(f,C,p)===!1||2===b))return C.abort();w="abort";for(i in{success:1,error:1,complete:1})C[i](p[i]);if(u=qn(jn,p,n,C)){C.readyState=1,l&&d.trigger("ajaxSend",[C,p]),p.async&&p.timeout>0&&(s=setTimeout(function(){C.abort("timeout")},p.timeout));try{b=1,u.send(y,k)}catch(N){if(!(2>b))throw N;k(-1,N)}}else k(-1,"No Transport");function k(e,n,r,i){var c,y,v,w,T,N=n;2!==b&&(b=2,s&&clearTimeout(s),u=t,a=i||"",C.readyState=e>0?4:0,c=e>=200&&300>e||304===e,r&&(w=Mn(p,C,r)),w=On(p,w,C,c),c?(p.ifModified&&(T=C.getResponseHeader("Last-Modified"),T&&(x.lastModified[o]=T),T=C.getResponseHeader("etag"),T&&(x.etag[o]=T)),204===e||"HEAD"===p.type?N="nocontent":304===e?N="notmodified":(N=w.state,y=w.data,v=w.error,c=!v)):(v=N,(e||!N)&&(N="error",0>e&&(e=0))),C.status=e,C.statusText=(n||N)+"",c?h.resolveWith(f,[y,N,C]):h.rejectWith(f,[C,N,v]),C.statusCode(m),m=t,l&&d.trigger(c?"ajaxSuccess":"ajaxError",[C,p,c?y:v]),g.fireWith(f,[C,N]),l&&(d.trigger("ajaxComplete",[C,p]),--x.active||x.event.trigger("ajaxStop")))}return C},getJSON:function(e,t,n){return x.get(e,t,n,"json")},getScript:function(e,n){return x.get(e,t,n,"script")}}),x.each(["get","post"],function(e,n){x[n]=function(e,r,i,o){return x.isFunction(r)&&(o=o||i,i=r,r=t),x.ajax({url:e,type:n,dataType:o,data:r,success:i})}});function Mn(e,n,r){var i,o,a,s,l=e.contents,u=e.dataTypes;while("*"===u[0])u.shift(),o===t&&(o=e.mimeType||n.getResponseHeader("Content-Type"));if(o)for(s in l)if(l[s]&&l[s].test(o)){u.unshift(s);break}if(u[0]in r)a=u[0];else{for(s in r){if(!u[0]||e.converters[s+" "+u[0]]){a=s;break}i||(i=s)}a=a||i}return a?(a!==u[0]&&u.unshift(a),r[a]):t}function On(e,t,n,r){var i,o,a,s,l,u={},c=e.dataTypes.slice();if(c[1])for(a in e.converters)u[a.toLowerCase()]=e.converters[a];o=c.shift();while(o)if(e.responseFields[o]&&(n[e.responseFields[o]]=t),!l&&r&&e.dataFilter&&(t=e.dataFilter(t,e.dataType)),l=o,o=c.shift())if("*"===o)o=l;else if("*"!==l&&l!==o){if(a=u[l+" "+o]||u["* "+o],!a)for(i in u)if(s=i.split(" "),s[1]===o&&(a=u[l+" "+s[0]]||u["* "+s[0]])){a===!0?a=u[i]:u[i]!==!0&&(o=s[0],c.unshift(s[1]));break}if(a!==!0)if(a&&e["throws"])t=a(t);else try{t=a(t)}catch(p){return{state:"parsererror",error:a?p:"No conversion from "+l+" to "+o}}}return{state:"success",data:t}}x.ajaxSetup({accepts:{script:"text/javascript, application/javascript, application/ecmascript, application/x-ecmascript"},contents:{script:/(?:java|ecma)script/},converters:{"text script":function(e){return x.globalEval(e),e}}}),x.ajaxPrefilter("script",function(e){e.cache===t&&(e.cache=!1),e.crossDomain&&(e.type="GET",e.global=!1)}),x.ajaxTransport("script",function(e){if(e.crossDomain){var n,r=a.head||x("head")[0]||a.documentElement;return{send:function(t,i){n=a.createElement("script"),n.async=!0,e.scriptCharset&&(n.charset=e.scriptCharset),n.src=e.url,n.onload=n.onreadystatechange=function(e,t){(t||!n.readyState||/loaded|complete/.test(n.readyState))&&(n.onload=n.onreadystatechange=null,n.parentNode&&n.parentNode.removeChild(n),n=null,t||i(200,"success"))},r.insertBefore(n,r.firstChild)},abort:function(){n&&n.onload(t,!0)}}}});var Fn=[],Bn=/(=)\?(?=&|$)|\?\?/;x.ajaxSetup({jsonp:"callback",jsonpCallback:function(){var e=Fn.pop()||x.expando+"_"+vn++;return this[e]=!0,e}}),x.ajaxPrefilter("json jsonp",function(n,r,i){var o,a,s,l=n.jsonp!==!1&&(Bn.test(n.url)?"url":"string"==typeof n.data&&!(n.contentType||"").indexOf("application/x-www-form-urlencoded")&&Bn.test(n.data)&&"data");return l||"jsonp"===n.dataTypes[0]?(o=n.jsonpCallback=x.isFunction(n.jsonpCallback)?n.jsonpCallback():n.jsonpCallback,l?n[l]=n[l].replace(Bn,"$1"+o):n.jsonp!==!1&&(n.url+=(bn.test(n.url)?"&":"?")+n.jsonp+"="+o),n.converters["script json"]=function(){return s||x.error(o+" was not called"),s[0]},n.dataTypes[0]="json",a=e[o],e[o]=function(){s=arguments},i.always(function(){e[o]=a,n[o]&&(n.jsonpCallback=r.jsonpCallback,Fn.push(o)),s&&x.isFunction(a)&&a(s[0]),s=a=t}),"script"):t});var Pn,Rn,Wn=0,$n=e.ActiveXObject&&function(){var e;for(e in Pn)Pn[e](t,!0)};function In(){try{return new e.XMLHttpRequest}catch(t){}}function zn(){try{return new e.ActiveXObject("Microsoft.XMLHTTP")}catch(t){}}x.ajaxSettings.xhr=e.ActiveXObject?function(){return!this.isLocal&&In()||zn()}:In,Rn=x.ajaxSettings.xhr(),x.support.cors=!!Rn&&"withCredentials"in Rn,Rn=x.support.ajax=!!Rn,Rn&&x.ajaxTransport(function(n){if(!n.crossDomain||x.support.cors){var r;return{send:function(i,o){var a,s,l=n.xhr();if(n.username?l.open(n.type,n.url,n.async,n.username,n.password):l.open(n.type,n.url,n.async),n.xhrFields)for(s in n.xhrFields)l[s]=n.xhrFields[s];n.mimeType&&l.overrideMimeType&&l.overrideMimeType(n.mimeType),n.crossDomain||i["X-Requested-With"]||(i["X-Requested-With"]="XMLHttpRequest");try{for(s in i)l.setRequestHeader(s,i[s])}catch(u){}l.send(n.hasContent&&n.data||null),r=function(e,i){var s,u,c,p;try{if(r&&(i||4===l.readyState))if(r=t,a&&(l.onreadystatechange=x.noop,$n&&delete Pn[a]),i)4!==l.readyState&&l.abort();else{p={},s=l.status,u=l.getAllResponseHeaders(),"string"==typeof l.responseText&&(p.text=l.responseText);try{c=l.statusText}catch(f){c=""}s||!n.isLocal||n.crossDomain?1223===s&&(s=204):s=p.text?200:404}}catch(d){i||o(-1,d)}p&&o(s,c,p,u)},n.async?4===l.readyState?setTimeout(r):(a=++Wn,$n&&(Pn||(Pn={},x(e).unload($n)),Pn[a]=r),l.onreadystatechange=r):r()},abort:function(){r&&r(t,!0)}}}});var Xn,Un,Vn=/^(?:toggle|show|hide)$/,Yn=RegExp("^(?:([+-])=|)("+w+")([a-z%]*)$","i"),Jn=/queueHooks$/,Gn=[nr],Qn={"*":[function(e,t){var n=this.createTween(e,t),r=n.cur(),i=Yn.exec(t),o=i&&i[3]||(x.cssNumber[e]?"":"px"),a=(x.cssNumber[e]||"px"!==o&&+r)&&Yn.exec(x.css(n.elem,e)),s=1,l=20;if(a&&a[3]!==o){o=o||a[3],i=i||[],a=+r||1;do s=s||".5",a/=s,x.style(n.elem,e,a+o);while(s!==(s=n.cur()/r)&&1!==s&&--l)}return i&&(a=n.start=+a||+r||0,n.unit=o,n.end=i[1]?a+(i[1]+1)*i[2]:+i[2]),n}]};function Kn(){return setTimeout(function(){Xn=t}),Xn=x.now()}function Zn(e,t,n){var r,i=(Qn[t]||[]).concat(Qn["*"]),o=0,a=i.length;for(;a>o;o++)if(r=i[o].call(n,t,e))return r}function er(e,t,n){var r,i,o=0,a=Gn.length,s=x.Deferred().always(function(){delete l.elem}),l=function(){if(i)return!1;var t=Xn||Kn(),n=Math.max(0,u.startTime+u.duration-t),r=n/u.duration||0,o=1-r,a=0,l=u.tweens.length;for(;l>a;a++)u.tweens[a].run(o);return s.notifyWith(e,[u,o,n]),1>o&&l?n:(s.resolveWith(e,[u]),!1)},u=s.promise({elem:e,props:x.extend({},t),opts:x.extend(!0,{specialEasing:{}},n),originalProperties:t,originalOptions:n,startTime:Xn||Kn(),duration:n.duration,tweens:[],createTween:function(t,n){var r=x.Tween(e,u.opts,t,n,u.opts.specialEasing[t]||u.opts.easing);return u.tweens.push(r),r},stop:function(t){var n=0,r=t?u.tweens.length:0;if(i)return this;for(i=!0;r>n;n++)u.tweens[n].run(1);return t?s.resolveWith(e,[u,t]):s.rejectWith(e,[u,t]),this}}),c=u.props;for(tr(c,u.opts.specialEasing);a>o;o++)if(r=Gn[o].call(u,e,c,u.opts))return r;return x.map(c,Zn,u),x.isFunction(u.opts.start)&&u.opts.start.call(e,u),x.fx.timer(x.extend(l,{elem:e,anim:u,queue:u.opts.queue})),u.progress(u.opts.progress).done(u.opts.done,u.opts.complete).fail(u.opts.fail).always(u.opts.always)}function tr(e,t){var n,r,i,o,a;for(n in e)if(r=x.camelCase(n),i=t[r],o=e[n],x.isArray(o)&&(i=o[1],o=e[n]=o[0]),n!==r&&(e[r]=o,delete e[n]),a=x.cssHooks[r],a&&"expand"in a){o=a.expand(o),delete e[r];for(n in o)n in e||(e[n]=o[n],t[n]=i)}else t[r]=i}x.Animation=x.extend(er,{tweener:function(e,t){x.isFunction(e)?(t=e,e=["*"]):e=e.split(" ");var n,r=0,i=e.length;for(;i>r;r++)n=e[r],Qn[n]=Qn[n]||[],Qn[n].unshift(t)},prefilter:function(e,t){t?Gn.unshift(e):Gn.push(e)}});function nr(e,t,n){var r,i,o,a,s,l,u=this,c={},p=e.style,f=e.nodeType&&nn(e),d=x._data(e,"fxshow");n.queue||(s=x._queueHooks(e,"fx"),null==s.unqueued&&(s.unqueued=0,l=s.empty.fire,s.empty.fire=function(){s.unqueued||l()}),s.unqueued++,u.always(function(){u.always(function(){s.unqueued--,x.queue(e,"fx").length||s.empty.fire()})})),1===e.nodeType&&("height"in t||"width"in t)&&(n.overflow=[p.overflow,p.overflowX,p.overflowY],"inline"===x.css(e,"display")&&"none"===x.css(e,"float")&&(x.support.inlineBlockNeedsLayout&&"inline"!==ln(e.nodeName)?p.zoom=1:p.display="inline-block")),n.overflow&&(p.overflow="hidden",x.support.shrinkWrapBlocks||u.always(function(){p.overflow=n.overflow[0],p.overflowX=n.overflow[1],p.overflowY=n.overflow[2]}));for(r in t)if(i=t[r],Vn.exec(i)){if(delete t[r],o=o||"toggle"===i,i===(f?"hide":"show"))continue;c[r]=d&&d[r]||x.style(e,r)}if(!x.isEmptyObject(c)){d?"hidden"in d&&(f=d.hidden):d=x._data(e,"fxshow",{}),o&&(d.hidden=!f),f?x(e).show():u.done(function(){x(e).hide()}),u.done(function(){var t;x._removeData(e,"fxshow");for(t in c)x.style(e,t,c[t])});for(r in c)a=Zn(f?d[r]:0,r,u),r in d||(d[r]=a.start,f&&(a.end=a.start,a.start="width"===r||"height"===r?1:0))}}function rr(e,t,n,r,i){return new rr.prototype.init(e,t,n,r,i)}x.Tween=rr,rr.prototype={constructor:rr,init:function(e,t,n,r,i,o){this.elem=e,this.prop=n,this.easing=i||"swing",this.options=t,this.start=this.now=this.cur(),this.end=r,this.unit=o||(x.cssNumber[n]?"":"px")},cur:function(){var e=rr.propHooks[this.prop];return e&&e.get?e.get(this):rr.propHooks._default.get(this)},run:function(e){var t,n=rr.propHooks[this.prop];return this.pos=t=this.options.duration?x.easing[this.easing](e,this.options.duration*e,0,1,this.options.duration):e,this.now=(this.end-this.start)*t+this.start,this.options.step&&this.options.step.call(this.elem,this.now,this),n&&n.set?n.set(this):rr.propHooks._default.set(this),this}},rr.prototype.init.prototype=rr.prototype,rr.propHooks={_default:{get:function(e){var t;return null==e.elem[e.prop]||e.elem.style&&null!=e.elem.style[e.prop]?(t=x.css(e.elem,e.prop,""),t&&"auto"!==t?t:0):e.elem[e.prop]},set:function(e){x.fx.step[e.prop]?x.fx.step[e.prop](e):e.elem.style&&(null!=e.elem.style[x.cssProps[e.prop]]||x.cssHooks[e.prop])?x.style(e.elem,e.prop,e.now+e.unit):e.elem[e.prop]=e.now}}},rr.propHooks.scrollTop=rr.propHooks.scrollLeft={set:function(e){e.elem.nodeType&&e.elem.parentNode&&(e.elem[e.prop]=e.now)}},x.each(["toggle","show","hide"],function(e,t){var n=x.fn[t];x.fn[t]=function(e,r,i){return null==e||"boolean"==typeof e?n.apply(this,arguments):this.animate(ir(t,!0),e,r,i)}}),x.fn.extend({fadeTo:function(e,t,n,r){return this.filter(nn).css("opacity",0).show().end().animate({opacity:t},e,n,r)},animate:function(e,t,n,r){var i=x.isEmptyObject(e),o=x.speed(t,n,r),a=function(){var t=er(this,x.extend({},e),o);(i||x._data(this,"finish"))&&t.stop(!0)};return a.finish=a,i||o.queue===!1?this.each(a):this.queue(o.queue,a)},stop:function(e,n,r){var i=function(e){var t=e.stop;delete e.stop,t(r)};return"string"!=typeof e&&(r=n,n=e,e=t),n&&e!==!1&&this.queue(e||"fx",[]),this.each(function(){var t=!0,n=null!=e&&e+"queueHooks",o=x.timers,a=x._data(this);if(n)a[n]&&a[n].stop&&i(a[n]);else for(n in a)a[n]&&a[n].stop&&Jn.test(n)&&i(a[n]);for(n=o.length;n--;)o[n].elem!==this||null!=e&&o[n].queue!==e||(o[n].anim.stop(r),t=!1,o.splice(n,1));(t||!r)&&x.dequeue(this,e)})},finish:function(e){return e!==!1&&(e=e||"fx"),this.each(function(){var t,n=x._data(this),r=n[e+"queue"],i=n[e+"queueHooks"],o=x.timers,a=r?r.length:0;for(n.finish=!0,x.queue(this,e,[]),i&&i.stop&&i.stop.call(this,!0),t=o.length;t--;)o[t].elem===this&&o[t].queue===e&&(o[t].anim.stop(!0),o.splice(t,1));for(t=0;a>t;t++)r[t]&&r[t].finish&&r[t].finish.call(this);delete n.finish})}});function ir(e,t){var n,r={height:e},i=0;for(t=t?1:0;4>i;i+=2-t)n=Zt[i],r["margin"+n]=r["padding"+n]=e;return t&&(r.opacity=r.width=e),r}x.each({slideDown:ir("show"),slideUp:ir("hide"),slideToggle:ir("toggle"),fadeIn:{opacity:"show"},fadeOut:{opacity:"hide"},fadeToggle:{opacity:"toggle"}},function(e,t){x.fn[e]=function(e,n,r){return this.animate(t,e,n,r)}}),x.speed=function(e,t,n){var r=e&&"object"==typeof e?x.extend({},e):{complete:n||!n&&t||x.isFunction(e)&&e,duration:e,easing:n&&t||t&&!x.isFunction(t)&&t};return r.duration=x.fx.off?0:"number"==typeof r.duration?r.duration:r.duration in x.fx.speeds?x.fx.speeds[r.duration]:x.fx.speeds._default,(null==r.queue||r.queue===!0)&&(r.queue="fx"),r.old=r.complete,r.complete=function(){x.isFunction(r.old)&&r.old.call(this),r.queue&&x.dequeue(this,r.queue)},r},x.easing={linear:function(e){return e},swing:function(e){return.5-Math.cos(e*Math.PI)/2}},x.timers=[],x.fx=rr.prototype.init,x.fx.tick=function(){var e,n=x.timers,r=0;for(Xn=x.now();n.length>r;r++)e=n[r],e()||n[r]!==e||n.splice(r--,1);n.length||x.fx.stop(),Xn=t},x.fx.timer=function(e){e()&&x.timers.push(e)&&x.fx.start()},x.fx.interval=13,x.fx.start=function(){Un||(Un=setInterval(x.fx.tick,x.fx.interval))},x.fx.stop=function(){clearInterval(Un),Un=null},x.fx.speeds={slow:600,fast:200,_default:400},x.fx.step={},x.expr&&x.expr.filters&&(x.expr.filters.animated=function(e){return x.grep(x.timers,function(t){return e===t.elem}).length}),x.fn.offset=function(e){if(arguments.length)return e===t?this:this.each(function(t){x.offset.setOffset(this,e,t)});var n,r,o={top:0,left:0},a=this[0],s=a&&a.ownerDocument;if(s)return n=s.documentElement,x.contains(n,a)?(typeof a.getBoundingClientRect!==i&&(o=a.getBoundingClientRect()),r=or(s),{top:o.top+(r.pageYOffset||n.scrollTop)-(n.clientTop||0),left:o.left+(r.pageXOffset||n.scrollLeft)-(n.clientLeft||0)}):o},x.offset={setOffset:function(e,t,n){var r=x.css(e,"position");"static"===r&&(e.style.position="relative");var i=x(e),o=i.offset(),a=x.css(e,"top"),s=x.css(e,"left"),l=("absolute"===r||"fixed"===r)&&x.inArray("auto",[a,s])>-1,u={},c={},p,f;l?(c=i.position(),p=c.top,f=c.left):(p=parseFloat(a)||0,f=parseFloat(s)||0),x.isFunction(t)&&(t=t.call(e,n,o)),null!=t.top&&(u.top=t.top-o.top+p),null!=t.left&&(u.left=t.left-o.left+f),"using"in t?t.using.call(e,u):i.css(u)}},x.fn.extend({position:function(){if(this[0]){var e,t,n={top:0,left:0},r=this[0];return"fixed"===x.css(r,"position")?t=r.getBoundingClientRect():(e=this.offsetParent(),t=this.offset(),x.nodeName(e[0],"html")||(n=e.offset()),n.top+=x.css(e[0],"borderTopWidth",!0),n.left+=x.css(e[0],"borderLeftWidth",!0)),{top:t.top-n.top-x.css(r,"marginTop",!0),left:t.left-n.left-x.css(r,"marginLeft",!0)}}},offsetParent:function(){return this.map(function(){var e=this.offsetParent||s;while(e&&!x.nodeName(e,"html")&&"static"===x.css(e,"position"))e=e.offsetParent;return e||s})}}),x.each({scrollLeft:"pageXOffset",scrollTop:"pageYOffset"},function(e,n){var r=/Y/.test(n);x.fn[e]=function(i){return x.access(this,function(e,i,o){var a=or(e);return o===t?a?n in a?a[n]:a.document.documentElement[i]:e[i]:(a?a.scrollTo(r?x(a).scrollLeft():o,r?o:x(a).scrollTop()):e[i]=o,t)},e,i,arguments.length,null)}});function or(e){return x.isWindow(e)?e:9===e.nodeType?e.defaultView||e.parentWindow:!1}x.each({Height:"height",Width:"width"},function(e,n){x.each({padding:"inner"+e,content:n,"":"outer"+e},function(r,i){x.fn[i]=function(i,o){var a=arguments.length&&(r||"boolean"!=typeof i),s=r||(i===!0||o===!0?"margin":"border");return x.access(this,function(n,r,i){var o;return x.isWindow(n)?n.document.documentElement["client"+e]:9===n.nodeType?(o=n.documentElement,Math.max(n.body["scroll"+e],o["scroll"+e],n.body["offset"+e],o["offset"+e],o["client"+e])):i===t?x.css(n,r,s):x.style(n,r,i,s)},n,a?i:t,a,null)}})}),x.fn.size=function(){return this.length},x.fn.andSelf=x.fn.addBack,"object"==typeof module&&module&&"object"==typeof module.exports?module.exports=x:(e.jQuery=e.$=x,"function"==typeof define&&define.amd&&define("jquery",[],function(){return x}))})(window);
/*! jQuery Mobile v1.4.5 | Copyright 2010, 2014 jQuery Foundation, Inc. | jquery.org/license */

(function(e,t,n){typeof define=="function"&&define.amd?define(["jquery"],function(r){return n(r,e,t),r.mobile}):n(e.jQuery,e,t)})(this,document,function(e,t,n,r){(function(e,t,n,r){function T(e){while(e&&typeof e.originalEvent!="undefined")e=e.originalEvent;return e}function N(t,n){var i=t.type,s,o,a,l,c,h,p,d,v;t=e.Event(t),t.type=n,s=t.originalEvent,o=e.event.props,i.search(/^(mouse|click)/)>-1&&(o=f);if(s)for(p=o.length,l;p;)l=o[--p],t[l]=s[l];i.search(/mouse(down|up)|click/)>-1&&!t.which&&(t.which=1);if(i.search(/^touch/)!==-1){a=T(s),i=a.touches,c=a.changedTouches,h=i&&i.length?i[0]:c&&c.length?c[0]:r;if(h)for(d=0,v=u.length;d<v;d++)l=u[d],t[l]=h[l]}return t}function C(t){var n={},r,s;while(t){r=e.data(t,i);for(s in r)r[s]&&(n[s]=n.hasVirtualBinding=!0);t=t.parentNode}return n}function k(t,n){var r;while(t){r=e.data(t,i);if(r&&(!n||r[n]))return t;t=t.parentNode}return null}function L(){g=!1}function A(){g=!0}function O(){E=0,v.length=0,m=!1,A()}function M(){L()}function _(){D(),c=setTimeout(function(){c=0,O()},e.vmouse.resetTimerDuration)}function D(){c&&(clearTimeout(c),c=0)}function P(t,n,r){var i;if(r&&r[t]||!r&&k(n.target,t))i=N(n,t),e(n.target).trigger(i);return i}function H(t){var n=e.data(t.target,s),r;!m&&(!E||E!==n)&&(r=P("v"+t.type,t),r&&(r.isDefaultPrevented()&&t.preventDefault(),r.isPropagationStopped()&&t.stopPropagation(),r.isImmediatePropagationStopped()&&t.stopImmediatePropagation()))}function B(t){var n=T(t).touches,r,i,o;n&&n.length===1&&(r=t.target,i=C(r),i.hasVirtualBinding&&(E=w++,e.data(r,s,E),D(),M(),d=!1,o=T(t).touches[0],h=o.pageX,p=o.pageY,P("vmouseover",t,i),P("vmousedown",t,i)))}function j(e){if(g)return;d||P("vmousecancel",e,C(e.target)),d=!0,_()}function F(t){if(g)return;var n=T(t).touches[0],r=d,i=e.vmouse.moveDistanceThreshold,s=C(t.target);d=d||Math.abs(n.pageX-h)>i||Math.abs(n.pageY-p)>i,d&&!r&&P("vmousecancel",t,s),P("vmousemove",t,s),_()}function I(e){if(g)return;A();var t=C(e.target),n,r;P("vmouseup",e,t),d||(n=P("vclick",e,t),n&&n.isDefaultPrevented()&&(r=T(e).changedTouches[0],v.push({touchID:E,x:r.clientX,y:r.clientY}),m=!0)),P("vmouseout",e,t),d=!1,_()}function q(t){var n=e.data(t,i),r;if(n)for(r in n)if(n[r])return!0;return!1}function R(){}function U(t){var n=t.substr(1);return{setup:function(){q(this)||e.data(this,i,{});var r=e.data(this,i);r[t]=!0,l[t]=(l[t]||0)+1,l[t]===1&&b.bind(n,H),e(this).bind(n,R),y&&(l.touchstart=(l.touchstart||0)+1,l.touchstart===1&&b.bind("touchstart",B).bind("touchend",I).bind("touchmove",F).bind("scroll",j))},teardown:function(){--l[t],l[t]||b.unbind(n,H),y&&(--l.touchstart,l.touchstart||b.unbind("touchstart",B).unbind("touchmove",F).unbind("touchend",I).unbind("scroll",j));var r=e(this),s=e.data(this,i);s&&(s[t]=!1),r.unbind(n,R),q(this)||r.removeData(i)}}}var i="virtualMouseBindings",s="virtualTouchID",o="vmouseover vmousedown vmousemove vmouseup vclick vmouseout vmousecancel".split(" "),u="clientX clientY pageX pageY screenX screenY".split(" "),a=e.event.mouseHooks?e.event.mouseHooks.props:[],f=e.event.props.concat(a),l={},c=0,h=0,p=0,d=!1,v=[],m=!1,g=!1,y="addEventListener"in n,b=e(n),w=1,E=0,S,x;e.vmouse={moveDistanceThreshold:10,clickDistanceThreshold:10,resetTimerDuration:1500};for(x=0;x<o.length;x++)e.event.special[o[x]]=U(o[x]);y&&n.addEventListener("click",function(t){var n=v.length,r=t.target,i,o,u,a,f,l;if(n){i=t.clientX,o=t.clientY,S=e.vmouse.clickDistanceThreshold,u=r;while(u){for(a=0;a<n;a++){f=v[a],l=0;if(u===r&&Math.abs(f.x-i)<S&&Math.abs(f.y-o)<S||e.data(u,s)===f.touchID){t.preventDefault(),t.stopPropagation();return}}u=u.parentNode}}},!0)})(e,t,n),function(e){e.mobile={}}(e),function(e,t){var r={touch:"ontouchend"in n};e.mobile.support=e.mobile.support||{},e.extend(e.support,r),e.extend(e.mobile.support,r)}(e),function(e,t,r){function l(t,n,i,s){var o=i.type;i.type=n,s?e.event.trigger(i,r,t):e.event.dispatch.call(t,i),i.type=o}var i=e(n),s=e.mobile.support.touch,o="touchmove scroll",u=s?"touchstart":"mousedown",a=s?"touchend":"mouseup",f=s?"touchmove":"mousemove";e.each("touchstart touchmove touchend tap taphold swipe swipeleft swiperight scrollstart scrollstop".split(" "),function(t,n){e.fn[n]=function(e){return e?this.bind(n,e):this.trigger(n)},e.attrFn&&(e.attrFn[n]=!0)}),e.event.special.scrollstart={enabled:!0,setup:function(){function s(e,n){r=n,l(t,r?"scrollstart":"scrollstop",e)}var t=this,n=e(t),r,i;n.bind(o,function(t){if(!e.event.special.scrollstart.enabled)return;r||s(t,!0),clearTimeout(i),i=setTimeout(function(){s(t,!1)},50)})},teardown:function(){e(this).unbind(o)}},e.event.special.tap={tapholdThreshold:750,emitTapOnTaphold:!0,setup:function(){var t=this,n=e(t),r=!1;n.bind("vmousedown",function(s){function a(){clearTimeout(u)}function f(){a(),n.unbind("vclick",c).unbind("vmouseup",a),i.unbind("vmousecancel",f)}function c(e){f(),!r&&o===e.target?l(t,"tap",e):r&&e.preventDefault()}r=!1;if(s.which&&s.which!==1)return!1;var o=s.target,u;n.bind("vmouseup",a).bind("vclick",c),i.bind("vmousecancel",f),u=setTimeout(function(){e.event.special.tap.emitTapOnTaphold||(r=!0),l(t,"taphold",e.Event("taphold",{target:o}))},e.event.special.tap.tapholdThreshold)})},teardown:function(){e(this).unbind("vmousedown").unbind("vclick").unbind("vmouseup"),i.unbind("vmousecancel")}},e.event.special.swipe={scrollSupressionThreshold:30,durationThreshold:1e3,horizontalDistanceThreshold:30,verticalDistanceThreshold:30,getLocation:function(e){var n=t.pageXOffset,r=t.pageYOffset,i=e.clientX,s=e.clientY;if(e.pageY===0&&Math.floor(s)>Math.floor(e.pageY)||e.pageX===0&&Math.floor(i)>Math.floor(e.pageX))i-=n,s-=r;else if(s<e.pageY-r||i<e.pageX-n)i=e.pageX-n,s=e.pageY-r;return{x:i,y:s}},start:function(t){var n=t.originalEvent.touches?t.originalEvent.touches[0]:t,r=e.event.special.swipe.getLocation(n);return{time:(new Date).getTime(),coords:[r.x,r.y],origin:e(t.target)}},stop:function(t){var n=t.originalEvent.touches?t.originalEvent.touches[0]:t,r=e.event.special.swipe.getLocation(n);return{time:(new Date).getTime(),coords:[r.x,r.y]}},handleSwipe:function(t,n,r,i){if(n.time-t.time<e.event.special.swipe.durationThreshold&&Math.abs(t.coords[0]-n.coords[0])>e.event.special.swipe.horizontalDistanceThreshold&&Math.abs(t.coords[1]-n.coords[1])<e.event.special.swipe.verticalDistanceThreshold){var s=t.coords[0]>n.coords[0]?"swipeleft":"swiperight";return l(r,"swipe",e.Event("swipe",{target:i,swipestart:t,swipestop:n}),!0),l(r,s,e.Event(s,{target:i,swipestart:t,swipestop:n}),!0),!0}return!1},eventInProgress:!1,setup:function(){var t,n=this,r=e(n),s={};t=e.data(this,"mobile-events"),t||(t={length:0},e.data(this,"mobile-events",t)),t.length++,t.swipe=s,s.start=function(t){if(e.event.special.swipe.eventInProgress)return;e.event.special.swipe.eventInProgress=!0;var r,o=e.event.special.swipe.start(t),u=t.target,l=!1;s.move=function(t){if(!o||t.isDefaultPrevented())return;r=e.event.special.swipe.stop(t),l||(l=e.event.special.swipe.handleSwipe(o,r,n,u),l&&(e.event.special.swipe.eventInProgress=!1)),Math.abs(o.coords[0]-r.coords[0])>e.event.special.swipe.scrollSupressionThreshold&&t.preventDefault()},s.stop=function(){l=!0,e.event.special.swipe.eventInProgress=!1,i.off(f,s.move),s.move=null},i.on(f,s.move).one(a,s.stop)},r.on(u,s.start)},teardown:function(){var t,n;t=e.data(this,"mobile-events"),t&&(n=t.swipe,delete t.swipe,t.length--,t.length===0&&e.removeData(this,"mobile-events")),n&&(n.start&&e(this).off(u,n.start),n.move&&i.off(f,n.move),n.stop&&i.off(a,n.stop))}},e.each({scrollstop:"scrollstart",taphold:"tap",swipeleft:"swipe.left",swiperight:"swipe.right"},function(t,n){e.event.special[t]={setup:function(){e(this).bind(n,e.noop)},teardown:function(){e(this).unbind(n)}}})}(e,this)});(function (u) { var k = function (a, c) { var h, g, k, m; k = a & 2147483648; m = c & 2147483648; h = a & 1073741824; g = c & 1073741824; a = (a & 1073741823) + (c & 1073741823); return h & g ? a ^ 2147483648 ^ k ^ m : h | g ? a & 1073741824 ? a ^ 3221225472 ^ k ^ m : a ^ 1073741824 ^ k ^ m : a ^ k ^ m }, l = function (a, c, h, g, l, m, b) { a = k(a, k(k(c & h | ~c & g, l), b)); return k(a << m | a >>> 32 - m, c) }, n = function (a, c, h, g, l, m, b) { a = k(a, k(k(c & g | h & ~g, l), b)); return k(a << m | a >>> 32 - m, c) }, p = function (a, c, h, g, l, m, b) { a = k(a, k(k(c ^ h ^ g, l), b)); return k(a << m | a >>> 32 - m, c) }, q = function (a, c, h, g, l, m, b) { a = k(a, k(k(h ^ (c | ~g), l), b)); return k(a << m | a >>> 32 - m, c) }, t = function (a) { var c = "", h, g; for (g = 0; 3 >= g; g++) h = a >>> 8 * g & 255, h = "0" + h.toString(16), c += h.substr(h.length - 2, 2); return c }; u.extend({ md5: function (a) { var c, h, g, r, m, b, d, e, f; a = a.replace(/\x0d\x0a/g, "\n"); c = ""; for (h = 0; h < a.length; h++) g = a.charCodeAt(h), 128 > g ? c += String.fromCharCode(g) : (127 < g && 2048 > g ? c += String.fromCharCode(g >> 6 | 192) : (c += String.fromCharCode(g >> 12 | 224), c += String.fromCharCode(g >> 6 & 63 | 128)), c += String.fromCharCode(g & 63 | 128)); h = c.length; a = h + 8; r = 16 * ((a - a % 64) / 64 + 1); a = Array(r - 1); for (b = 0; b < h;) g = (b - b % 4) / 4, m = b % 4 * 8, a[g] |= c.charCodeAt(b) << m, b++; g = (b - b % 4) / 4; a[g] |= 128 << b % 4 * 8; a[r - 2] = h << 3; a[r - 1] = h >>> 29; b = 1732584193; d = 4023233417; e = 2562383102; f = 271733878; for (c = 0; c < a.length; c += 16) h = b, g = d, r = e, m = f, b = l(b, d, e, f, a[c + 0], 7, 3614090360), f = l(f, b, d, e, a[c + 1], 12, 3905402710), e = l(e, f, b, d, a[c + 2], 17, 606105819), d = l(d, e, f, b, a[c + 3], 22, 3250441966), b = l(b, d, e, f, a[c + 4], 7, 4118548399), f = l(f, b, d, e, a[c + 5], 12, 1200080426), e = l(e, f, b, d, a[c + 6], 17, 2821735955), d = l(d, e, f, b, a[c + 7], 22, 4249261313), b = l(b, d, e, f, a[c + 8], 7, 1770035416), f = l(f, b, d, e, a[c + 9], 12, 2336552879), e = l(e, f, b, d, a[c + 10], 17, 4294925233), d = l(d, e, f, b, a[c + 11], 22, 2304563134), b = l(b, d, e, f, a[c + 12], 7, 1804603682), f = l(f, b, d, e, a[c + 13], 12, 4254626195), e = l(e, f, b, d, a[c + 14], 17, 2792965006), d = l(d, e, f, b, a[c + 15], 22, 1236535329), b = n(b, d, e, f, a[c + 1], 5, 4129170786), f = n(f, b, d, e, a[c + 6], 9, 3225465664), e = n(e, f, b, d, a[c + 11], 14, 643717713), d = n(d, e, f, b, a[c + 0], 20, 3921069994), b = n(b, d, e, f, a[c + 5], 5, 3593408605), f = n(f, b, d, e, a[c + 10], 9, 38016083), e = n(e, f, b, d, a[c + 15], 14, 3634488961), d = n(d, e, f, b, a[c + 4], 20, 3889429448), b = n(b, d, e, f, a[c + 9], 5, 568446438), f = n(f, b, d, e, a[c + 14], 9, 3275163606), e = n(e, f, b, d, a[c + 3], 14, 4107603335), d = n(d, e, f, b, a[c + 8], 20, 1163531501), b = n(b, d, e, f, a[c + 13], 5, 2850285829), f = n(f, b, d, e, a[c + 2], 9, 4243563512), e = n(e, f, b, d, a[c + 7], 14, 1735328473), d = n(d, e, f, b, a[c + 12], 20, 2368359562), b = p(b, d, e, f, a[c + 5], 4, 4294588738), f = p(f, b, d, e, a[c + 8], 11, 2272392833), e = p(e, f, b, d, a[c + 11], 16, 1839030562), d = p(d, e, f, b, a[c + 14], 23, 4259657740), b = p(b, d, e, f, a[c + 1], 4, 2763975236), f = p(f, b, d, e, a[c + 4], 11, 1272893353), e = p(e, f, b, d, a[c + 7], 16, 4139469664), d = p(d, e, f, b, a[c + 10], 23, 3200236656), b = p(b, d, e, f, a[c + 13], 4, 681279174), f = p(f, b, d, e, a[c + 0], 11, 3936430074), e = p(e, f, b, d, a[c + 3], 16, 3572445317), d = p(d, e, f, b, a[c + 6], 23, 76029189), b = p(b, d, e, f, a[c + 9], 4, 3654602809), f = p(f, b, d, e, a[c + 12], 11, 3873151461), e = p(e, f, b, d, a[c + 15], 16, 530742520), d = p(d, e, f, b, a[c + 2], 23, 3299628645), b = q(b, d, e, f, a[c + 0], 6, 4096336452), f = q(f, b, d, e, a[c + 7], 10, 1126891415), e = q(e, f, b, d, a[c + 14], 15, 2878612391), d = q(d, e, f, b, a[c + 5], 21, 4237533241), b = q(b, d, e, f, a[c + 12], 6, 1700485571), f = q(f, b, d, e, a[c + 3], 10, 2399980690), e = q(e, f, b, d, a[c + 10], 15, 4293915773), d = q(d, e, f, b, a[c + 1], 21, 2240044497), b = q(b, d, e, f, a[c + 8], 6, 1873313359), f = q(f, b, d, e, a[c + 15], 10, 4264355552), e = q(e, f, b, d, a[c + 6], 15, 2734768916), d = q(d, e, f, b, a[c + 13], 21, 1309151649), b = q(b, d, e, f, a[c + 4], 6, 4149444226), f = q(f, b, d, e, a[c + 11], 10, 3174756917), e = q(e, f, b, d, a[c + 2], 15, 718787259), d = q(d, e, f, b, a[c + 9], 21, 3951481745), b = k(b, h), d = k(d, g), e = k(e, r), f = k(f, m); return (t(b) + t(d) + t(e) + t(f)).toLowerCase() } }) })(jQuery);/*!
* =====================================================
*Fui v1.0.1
* =====================================================
*/
(function(a,g){Date.prototype.DateAdd=function(j,i){var h=this;switch(j){case"s":return new Date(Date.parse(h)+(1000*i));case"n":return new Date(Date.parse(h)+(60000*i));case"h":return new Date(Date.parse(h)+(3600000*i));case"d":return new Date(Date.parse(h)+(86400000*i));case"w":return new Date(Date.parse(h)+((86400000*7)*i));case"q":return new Date(h.getFullYear(),(h.getMonth())+i*3,h.getDate(),h.getHours(),h.getMinutes(),h.getSeconds());case"m":return new Date(h.getFullYear(),(h.getMonth())+i,h.getDate(),h.getHours(),h.getMinutes(),h.getSeconds());case"y":return new Date((h.getFullYear()+i),h.getMonth(),h.getDate(),h.getHours(),h.getMinutes(),h.getSeconds())}};Date.prototype.DateDiff=function(j,h){var i=this;if(typeof h=="string"){h=fui.date.parse(h)}switch(j){case"s":return parseInt((h-i)/1000);case"n":return parseInt((h-i)/60000);case"h":return parseInt((h-i)/3600000);case"d":return parseInt((h-i)/86400000);case"w":return parseInt((h-i)/(86400000*7));case"m":return(h.getMonth()+1)+((h.getFullYear()-i.getFullYear())*12)-(i.getMonth()+1);case"y":return h.getFullYear()-i.getFullYear()}};Date.prototype.MaxDayOfDate=function(){var k=this;var h=k.toArray();var i=(new Date(h[0],h[1]+1,1));var j=i.DateAdd("m",1);var l=dateDiff(i.Format("yyyy-MM-dd"),j.Format("yyyy-MM-dd"));return l};Date.prototype.isLeapYear=function(){return(0==this.getYear()%4&&((this.getYear()%100!=0)||(this.getYear()%400==0)))};var d;var c;a("body").on("tap",function(h){var j=g.fui.now();var i=h.target||h.srcElement;if(d&&d===i){if(c&&(j-c)<300){var k={center:{x:h.pageX,y:h.pageY}};g.fui.trigger(i,"doubletap",k)}}c=g.fui.now();d=i;return false});var f=/matrix(3d)?\((.+?)\)/;var b={};a.each(["Boolean","Number","String","Function","Array","Date","RegExp","Object","Error"],function(h,j){b["[object "+j+"]"]=j.toLowerCase()});var e=a.mobile.support.touch;g.fui=g.fui||{};g.fui={event:{supportTouch:e,start:e?"touchstart":"mousedown",end:e?"touchend":"mouseup",move:e?"touchmove":"mousemove",cancel:"touchcancel"},options:{gestureConfig:{tap:true,doubletap:false,longtap:false,hold:false,flick:true,swipe:true,drag:true,pinch:false}},now:Date.now||function(){return +new Date()},slice:[].slice,pageid:function(){if(!fui.nav){return"no"}var h=fui.nav.page.data[fui.nav.page.activeid]||{};return(h.op||{}).pageid||"no"},guid:function(l){l=l||"_";var h="";for(var j=1;j<=32;j++){var k=Math.floor(Math.random()*16).toString(16);h+=k;if((j==8)||(j==12)||(j==16)||(j==20)){h+=l}}return h},date:{isLeapYear:function(h){return(h%4==0&&h%100!=0)||(h%400==0)},parse:function(i){var h=i;if(typeof i==="string"){if(i.indexOf("/Date(")>-1){h=new Date(parseInt(i.replace("/Date(","").replace(")/",""),10))}else{h=new Date(Date.parse(i.replace(/-/g,"/").replace("T"," ").split(".")[0]))}}return h},format:function(m,i){if(!m){return""}var h=g.fui.date.parse(m);var l={"M+":h.getMonth()+1,"d+":h.getDate(),"h+":h.getHours(),"m+":h.getMinutes(),"s+":h.getSeconds(),"q+":Math.floor((h.getMonth()+3)/3),S:h.getMilliseconds()};if(/(y+)/.test(i)){i=i.replace(RegExp.$1,(h.getFullYear()+"").substr(4-RegExp.$1.length))}for(var j in l){if(new RegExp("("+j+")").test(i)){i=i.replace(RegExp.$1,RegExp.$1.length==1?l[j]:("00"+l[j]).substr((""+l[j]).length))}}return i},get:function(h,l,j){var i=new Date();if(!!l){i=i.DateAdd(l,j)}var k=g.fui.date.format(i,h);return k}},isArray:Array.isArray||function(h){return h instanceof Array},later:function(l,p,h,j){p=p||0;var n=l;var i=j;var k;var o;if(typeof l==="string"){n=h[l]}k=function(){n.apply(h,fui.isArray(i)?i:[i])};o=setTimeout(k,p);return{id:o,cancel:function(){clearTimeout(o)}}},createMask:function(){if(a(".f-backdrop").length==0){var h='<div class="f-backdrop" ></div>';a("body").append(h);a(".f-backdrop").on("tap",function(){a(this).fadeOut();a(".f-pop.active").removeClass("active");return false})}},showMask:function(){a(".f-backdrop").fadeIn()},hideMask:function(){a(".f-backdrop").fadeOut()},getStyles:function(h,i){var j=h.ownerDocument.defaultView.getComputedStyle(h,null);if(i){return j.getPropertyValue(i)||j[i]}return j},parseTranslateMatrix:function(l,j){var i=l.match(f);var h=i&&i[1];if(i){i=i[2].split(",");if(h==="3d"){i=i.slice(12,15)}else{i.push(0);i=i.slice(4,7)}}else{i=[0,0,0]}var k={x:parseFloat(i[0]),y:parseFloat(i[1]),z:parseFloat(i[2])};if(j&&k.hasOwnProperty(j)){return k[j]}return k},type:function(h){return h==null?String(h):b[{}.toString.call(h)]||"object"},isFunction:function(h){return fui.type(h)==="function"},trigger:function(h,j,i){if(h){h.dispatchEvent(new CustomEvent(j,{detail:i,bubbles:true,cancelable:true}))}return this},hooks:{},addAction:function(j,h){var i=fui.hooks[j];if(!i){i=[]}h.index=h.index||1000;i.push(h);i.sort(function(k,l){return k.index-l.index});fui.hooks[j]=i;return fui.hooks[j]},doAction:function(i,h){if(fui.isFunction(h)){a.each(fui.hooks[i],h)}else{a.each(fui.hooks[i],function(k,j){return !j.handle()})}},offset:function(i){var h={top:0,left:0};if(typeof i.getBoundingClientRect!==undefined){h=i.getBoundingClientRect()}return{top:h.top+g.pageYOffset-i.clientTop,left:h.left+g.pageXOffset-i.clientLeft}},includeJs:function(j){var h=document.getElementsByTagName("HEAD").item(0);var i=document.createElement("script");i.language="javascript";i.type="text/javascript";i.text=j;h.appendChild(i)},includeCss:function(j){var i=document.getElementsByTagName("head").item(0);var k=document.createElement("style");k.type="text/css";try{k.appendChild(document.createTextNode(j))}catch(h){k.style.cssText=j}i.appendChild(k)},data:{}}})(window.jQuery,this);(function(c){var d=false,b=/xyz/.test(function(){xyz})?/\b_super\b/:/.*/;var a=function(){};a.extend=function(h){var e=this.prototype;d=true;var i=new this();d=false;for(var g in h){i[g]=typeof h[g]=="function"&&typeof e[g]=="function"&&b.test(h[g])?(function(k,j){return function(){var m=this._super;this._super=e[k];var l=j.apply(this,arguments);this._super=m;return l}})(g,h[g]):h[g]}function f(){if(!d&&this.init){this.init.apply(this,arguments)}}f.prototype=i;f.prototype.constructor=f;f.extend=arguments.callee;return f};c.Class=a})(window.fui);(function(b){if(!b.requestAnimationFrame){var a=0;b.requestAnimationFrame=b.webkitRequestAnimationFrame||function(c,e){var d=new Date().getTime();var g=Math.max(0,16.7-(d-a));var f=b.setTimeout(function(){c(d+g)},g);a=d+g;return f};b.cancelAnimationFrame=b.webkitCancelAnimationFrame||b.webkitCancelRequestAnimationFrame||function(c){clearTimeout(c)}}}(window));(function(a,b){b.validator={validReg:function(d,e,c){var f={code:true,msg:""};if(!e.test(d)){f.code=false;f.msg=c}return f},validRegOrNull:function(d,e,c){var f={code:true,msg:""};if(d==null||d==undefined||d.length==0){return f}if(!e.test(d)){f.code=false;f.msg=c}return f},isNotNull:function(c){var d={code:true,msg:""};c=a.trim(c);if(c==null||c==undefined||c.length==0){d.code=false;d.msg="不能为空"}return d},isNum:function(c){return b.validator.validReg(c,/^[-+]?\d+$/,"必须为数字")},isNumOrNull:function(c){return b.validator.validRegOrNull(c,/^[-+]?\d+$/,"数字或空")},isEmail:function(c){return b.validator.validReg(c,/^\w{3,}@\w+(\.\w+)+$/,"必须为E-mail格式")},isEmailOrNull:function(c){return b.validator.validRegOrNull(c,/^\w{3,}@\w+(\.\w+)+$/,"必须为E-mail格式或空")},isEnglishStr:function(c){return b.validator.validReg(c,/^[a-z,A-Z]+$/,"必须为英文字符串")},isEnglishStrOrNull:function(c){return b.validator.validRegOrNull(c,/^[a-z,A-Z]+$/,"必须为英文字符串或空")},isTelephone:function(c){return b.validator.validReg(c,/^(\d{3,4}\-)?[1-9]\d{6,7}$/,"必须为电话格式")},isTelephoneOrNull:function(c){return b.validator.validRegOrNull(c,/^(\d{3,4}\-)?[1-9]\d{6,7}$/,"必须为电话格式或空")},isMobile:function(c){return b.validator.validReg(c,/^(\+\d{2,3}\-)?\d{11}$/,"必须为手机格式")},isMobileOrnull:function(c){return b.validator.validRegOrNull(c,/^(\+\d{2,3}\-)?\d{11}$/,"必须为手机格式或空")},isMobileOrPhone:function(c){var d={code:true,msg:""};if(!b.validator.isTelephone(c).code&&!b.validator.isMobile(c).code){d.code=false;d.msg="为电话格式或手机格式"}return d},isMobileOrPhoneOrNull:function(c){var d={code:true,msg:""};if(b.validator.isNotNull(c).code&&!b.validator.isTelephone(c).code&&!b.validator.isMobile(c).code){d.code=false;d.msg="为电话格式或手机格式或空"}return d},isUri:function(c){return b.validator.validReg(c,/^http:\/\/[a-zA-Z0-9]+\.[a-zA-Z0-9]+[\/=\?%\-&_~`@[\]\':+!]*([^<>\"\"])*$/,"必须为网址格式")},isUriOrNull:function(c){return b.validator.validRegOrNull(c,/^http:\/\/[a-zA-Z0-9]+\.[a-zA-Z0-9]+[\/=\?%\-&_~`@[\]\':+!]*([^<>\"\"])*$/,"必须为网址格式或空")},isDate:function(c){return b.validator.validReg(c,/^(\d{1,4})(-|\/)(\d{1,2})\2(\d{1,2})$/,"必须为日期格式")},isDateOrNull:function(c){return b.validator.validRegOrNull(c,/^(\d{1,4})(-|\/)(\d{1,2})\2(\d{1,2})$/,"必须为日期格式或空")},isDateTime:function(c){return b.validator.validReg(c,/^(\d{1,4})(-|\/)(\d{1,2})\2(\d{1,2}) (\d{1,2}):(\d{1,2}):(\d{1,2})$/,"必须为日期时间格式")},isDateTimeOrNull:function(c){return b.validator.validRegOrNull(c,/^(\d{1,4})(-|\/)(\d{1,2})\2(\d{1,2}) (\d{1,2}):(\d{1,2}):(\d{1,2})$/,"必须为日期时间格式")},isTime:function(c){return b.validator.validReg(c,/^((20|21|22|23|[0-1]\d)\:[0-5][0-9])(\:[0-5][0-9])?$/,"必须为时间格式")},isTimeOrNull:function(c){return b.validator.validRegOrNull(c,/^((20|21|22|23|[0-1]\d)\:[0-5][0-9])(\:[0-5][0-9])?$/,"必须为时间格式或空")},isChinese:function(c){return b.validator.validReg(c,/^[\u0391-\uFFE5]+$/,"必须为中文")},isChineseOrNull:function(c){return b.validator.validRegOrNull(c,/^[\u0391-\uFFE5]+$/,"必须为中文或空")},isZip:function(c){return b.validator.validReg(c,/^\d{6}$/,"必须为邮编格式")},isZipOrNull:function(c){return b.validator.validRegOrNull(c,/^\d{6}$/,"必须为邮编格式或空")},isDouble:function(c){return b.validator.validReg(c,/^[-\+]?\d+(\.\d+)?$/,"必须为小数")},isDoubleOrNull:function(c){return b.validator.validRegOrNull(c,/^[-\+]?\d+(\.\d+)?$/,"必须为小数或空")},isIDCard:function(c){return b.validator.validReg(c,/^\d{15}(\d{2}[A-Za-z0-9;])?$/,"必须为身份证格式")},isIDCardOrNull:function(c){return b.validator.validRegOrNull(c,/^\d{15}(\d{2}[A-Za-z0-9;])?$/,"必须为身份证格式或空")},isIP:function(d){var f={code:true,msg:""};var e=/^(\d+)\.(\d+)\.(\d+)\.(\d+)$/g;var c=false;if(e.test(d)){if(RegExp.$1<256&&RegExp.$2<256&&RegExp.$3<256&&RegExp.$4<256){c=true}}if(!c){f.code=false;f.msg="必须为IP格式"}return f},isIPOrNull:function(c){var d={code:true,msg:""};if(b.validator.isNotNull(c)&&!b.validator.isIP(c).code){d.code=false;d.msg="必须为IP格式或空"}return d},isLenNum:function(d,c){var f={code:true,msg:""};var e=/^[0-9]+$/;d=a.trim(d);if(d.length>c||!e.test(d)){f.code=false;f.msg="必须为"+c+"位数字"}return f},isLenNumOrNull:function(d,c){var e={code:true,msg:""};if(b.validator.isNotNull(d).code&&!b.validator.isLenNum(d)){e.code=false;e.msg="必须为"+c+"位数字或空"}return e},isLenStr:function(d,c){var e={code:true,msg:""};d=a.trim(d);if(!b.validator.isNotNull(d).code||d.length>c){e.code=false;e.msg="必须小于等于"+c+"位字符"}return e},isLenStrOrNull:function(d,c){var e={code:true,msg:""};d=a.trim(d);if(b.validator.isNotNull(d).code&&d.length>c){e.code=false;e.msg="必须小于等于"+c+"位字符或空"}return e}}})(window.jQuery,window.fui);(function(k,B){k.gestures={session:{}};k.preventDefault=function(C){C.preventDefault()};k.stopPropagation=function(C){C.stopPropagation()};k.addGesture=function(C){return k.addAction("gestures",C)};var w=Math.round;var a=Math.abs;var y=Math.sqrt;var b=Math.atan;var c=Math.atan2;var n=function(C,D,E){if(!E){E=["x","y"]}var F=D[E[0]]-C[E[0]];var G=D[E[1]]-C[E[1]];return y((F*F)+(G*G))};var q=function(E,C){if(E.length>=2&&C.length>=2){var D=["pageX","pageY"];return n(C[1],C[0],D)/n(E[1],E[0],D)}return 1};var l=function(C,D,E){if(!E){E=["x","y"]}var F=D[E[0]]-C[E[0]];var G=D[E[1]]-C[E[1]];return c(G,F)*180/Math.PI};var m=function(C,D){if(C===D){return""}if(a(C)>=a(D)){return C>0?"left":"right"}return D>0?"up":"down"};var p=function(E,C){var D=["pageX","pageY"];return l(C[1],C[0],D)-l(E[1],E[0],D)};var s=function(C,D,E){return{x:D/C||0,y:E/C||0}};var j=function(C,D){if(k.gestures.stoped){return}k.doAction("gestures",function(F,E){if(!k.gestures.stoped){if(k.options.gestureConfig[E.name]!==false){E.handle(C,D)}}})};var u=function(C,D){while(C){if(C==D){return true}C=C.parentNode}return false};var A=function(H,D,F){var E=[];var J=[];var C=0;while(C<H.length){var I=D?H[C][D]:H[C];if(J.indexOf(I)<0){E.push(H[C])}J[C]=I;C++}if(F){if(!D){E=E.sort()}else{E=E.sort(function G(K,L){return K[D]>L[D]})}}return E};var o=function(D){var E=D.length;if(E===1){return{x:w(D[0].pageX),y:w(D[0].pageY)}}var F=0;var G=0;var C=0;while(C<E){F+=D[C].pageX;G+=D[C].pageY;C++}return{x:w(F/E),y:w(G/E)}};var v=function(){return k.options.gestureConfig.pinch};var i=function(D){var E=[];var C=0;while(C<D.touches.length){E[C]={pageX:w(D.touches[C].pageX),pageY:w(D.touches[C].pageY)};C++}return{timestamp:k.now(),gesture:D.gesture,touches:E,center:o(D.touches),deltaX:D.deltaX,deltaY:D.deltaY}};var e=function(H){var G=k.gestures.session;var C=H.center;var D=G.offsetDelta||{};var E=G.prevDelta||{};var F=G.prevTouch||{};if(H.gesture.type===k.event.start||H.gesture.type===k.event.end){E=G.prevDelta={x:F.deltaX||0,y:F.deltaY||0};D=G.offsetDelta={x:C.x,y:C.y}}H.deltaX=E.x+(C.x-D.x);H.deltaY=E.y+(C.y-D.y)};var g=function(H){var G=k.gestures.session;var I=H.touches;var J=I.length;if(!G.firstTouch){G.firstTouch=i(H)}if(v()&&J>1&&!G.firstMultiTouch){G.firstMultiTouch=i(H)}else{if(J===1){G.firstMultiTouch=false}}var E=G.firstTouch;var D=G.firstMultiTouch;var F=D?D.center:E.center;var C=H.center=o(I);H.timestamp=k.now();H.deltaTime=H.timestamp-E.timestamp;H.angle=l(F,C);H.distance=n(F,C);e(H);H.offsetDirection=m(H.deltaX,H.deltaY);H.scale=D?q(D.touches,I):1;H.rotation=D?p(D.touches,I):0;f(H)};var d=25;var f=function(I){var H=k.gestures.session;var G=H.lastInterval||I;var C=I.timestamp-G.timestamp;var K;var L;var M;var F;if(I.gesture.type!=k.event.cancel&&(C>d||G.velocity===undefined)){var D=G.deltaX-I.deltaX;var E=G.deltaY-I.deltaY;var J=s(C,D,E);L=J.x;M=J.y;K=(a(J.x)>a(J.y))?J.x:J.y;F=m(D,E)||G.direction;H.lastInterval=I}else{K=G.velocity;L=G.velocityX;M=G.velocityY;F=G.direction}I.velocity=K;I.velocityX=L;I.velocityY=M;I.direction=F};var z={};var h=function(D){for(var C=0;C<D.length;C++){!D.identifier&&(D.identifier=0)}return D};var r=function(G,K){var C=h(k.slice.call(G.touches||[G]));var M=G.type;var J=[];var D=[];if((M===k.event.start||M===k.event.move)&&C.length===1){z[C[0].identifier]=true;J=C;D=C;K.target=G.target}else{var H=0;var J=[];var D=[];var E=h(k.slice.call(G.changedTouches||[G]));K.target=G.target;var I=k.gestures.session.target||G.target;J=C.filter(function(N){return u(N.target,I)});if(M===k.event.start){H=0;while(H<J.length){z[J[H].identifier]=true;H++}}H=0;while(H<E.length){if(z[E[H].identifier]){D.push(E[H])}if(M===k.event.end||M===k.event.cancel){delete z[E[H].identifier]}H++}if(!D.length){return false}}J=A(J.concat(D),"identifier",true);var L=J.length;var F=D.length;if(M===k.event.start&&L-F===0){K.isFirst=true;k.gestures.touch=k.gestures.session={target:G.target}}K.isFinal=((M===k.event.end||M===k.event.cancel)&&(L-F===0));K.touches=J;K.changedTouches=D;return true};var t=function(C){var D={gesture:C};var E=r(C,D);if(!E){return}g(D);j(C,D);k.gestures.session.prevTouch=D;if(C.type===k.event.end&&!k.event.supportTouch){k.gestures.touch=k.gestures.session={}}};B.addEventListener(k.event.start,t);B.addEventListener(k.event.move,t,{passive:false});B.addEventListener(k.event.end,t);B.addEventListener(k.event.cancel,t);k.isScrolling=false;var x=null;B.addEventListener("scroll",function(){k.isScrolling=true;x&&clearTimeout(x);x=setTimeout(function(){k.isScrolling=false},250)})})(window.fui,window);(function(a,c){var b=function(d,f){var e=a.gestures.session;switch(d.type){case a.event.start:break;case a.event.move:if(!f.direction||!e.target){return}if(e.lockDirection&&e.startDirection){if(e.startDirection&&e.startDirection!==f.direction){if(e.startDirection==="up"||e.startDirection==="down"){f.direction=f.deltaY<0?"up":"down"}else{f.direction=f.deltaX<0?"left":"right"}}}if(!e.drag){e.drag=true;a.trigger(e.target,c+"start",f)}a.trigger(e.target,c,f);a.trigger(e.target,c+f.direction,f);break;case a.event.end:case a.event.cancel:if(e.drag&&f.isFinal){a.trigger(e.target,c+"end",f)}break}};a.addGesture({name:c,index:20,handle:b,options:{fingers:1}})})(fui,"drag");(function(b,d){var a=0;var c=function(e,i){var h=b.gestures.session;var g=this.options;var f=b.now();switch(e.type){case b.event.move:if(f-a>300){a=f;h.flickStart=i.center}break;case b.event.end:case b.event.cancel:i.flick=false;if(h.flickStart&&g.flickMaxTime>(f-a)&&i.distance>g.flickMinDistince){i.flick=true;i.flickTime=f-a;i.flickDistanceX=i.center.x-h.flickStart.x;i.flickDistanceY=i.center.y-h.flickStart.y;b.trigger(h.target,d,i);b.trigger(h.target,d+i.direction,i)}break}};b.addGesture({name:d,index:5,handle:c,options:{flickMaxTime:200,flickMinDistince:10}})})(fui,"flick");(function(a,c){var d;var b=function(e,h){var g=a.gestures.session;var f=this.options;switch(e.type){case a.event.start:if(a.options.gestureConfig.hold){d&&clearTimeout(d);d=setTimeout(function(){h.hold=true;a.trigger(g.target,c,h)},f.holdTimeout)}break;case a.event.move:break;case a.event.end:case a.evetn.cancel:if(d){clearTimeout(d)&&(d=null);a.trigger(g.target,"release",h)}break}};a.addGesture({name:c,index:10,handle:b,options:{fingers:1,holdTimeout:0}})})(fui,"hold");(function(a,c){var b=function(d,k){var f=this.options;var j=a.gestures.session;switch(d.type){case a.event.start:break;case a.event.move:if(a.options.gestureConfig.pinch){if(k.touches.length<2){return}if(!j.pinch){j.pinch=true;a.trigger(j.target,c+"start",k)}a.trigger(j.target,c,k);var h=k.scale;var g=k.rotation;var e=typeof k.lastScale==="undefined"?1:k.lastScale;var i=1e-12;if(h>e){e=h-i;a.trigger(j.target,c+"out",k)}else{if(h<e){e=h+i;a.trigger(j.target,c+"in",k)}}if(Math.abs(g)>f.minRotationAngle){a.trigger(j.target,"rotate",k)}}break;case a.event.end:case a.event.cancel:if(a.options.gestureConfig.pinch&&j.pinch&&k.touches.length===2){j.pinch=false;a.trigger(j.target,c+"end",k)}break}};a.addGesture({name:c,index:10,handle:b,options:{minRotationAngle:0}})})(fui,"pinch");(function(a,c){var b=function(d,g){var f=a.gestures.session;if(d.type===a.event.end||d.type===a.event.cancel){var e=this.options;g.swipe=false;if(g.direction&&e.swipeMaxTime>g.deltaTime&&g.distance>e.swipeMinDistince){g.swipe=true;a.trigger(f.target,c,g);a.trigger(f.target,c+g.direction,g)}}};a.addGesture({name:c,index:10,handle:b,options:{swipeMaxTime:300,swipeMinDistince:18}})})(fui,"fswipe");(function(l,t,o){var m=l([]),q=l.resize=l.extend(l.resize,{}),u,w="setTimeout",v="resize",p=v+"-special-event",n="delay",r="throttleWindow";q[n]=250;q[r]=true;l.event.special[v]={setup:function(){if(!q[r]&&this[w]){return false}var a=l(this);m=m.add(a);l.data(this,p,{w:a.width(),h:a.height()});if(m.length===1){s()}},teardown:function(){if(!q[r]&&this[w]){return false}var a=l(this);m=m.not(a);a.removeData(p);if(!m.length){clearTimeout(u)}},add:function(a){if(!q[r]&&this[w]){return false}var c;function b(h,d,e){var f=l(this),g=l.data(this,p);g.w=d!==o?d:f.width();g.h=e!==o?e:f.height();c.apply(this,arguments)}if(l.isFunction(a)){c=a;return b}else{c=a.handler;a.handler=b}}};function s(){u=t[w](function(){m.each(function(){var c=l(this),b=c.width(),a=c.height(),d=l.data(this,p);if(b!==d.w||a!==d.h){c.trigger(v,[d.w=b,d.h=a])}});s()},q[n])}})(jQuery,this);(function(a,d){var g={};var f=0;var e=0;var c=false;var b={doQueue:function(){if(!c){c=true;var h=g[e];if(h){b[h.type](h.op,function(){delete g[e];e++;c=false;if(e<f){b.doQueue()}})}else{c=false;if(e<f){e++;b.doQueue()}}}},open:function(k,j){var l=null;if(d.nav.page.data[k.id]){l=d.nav.page.data[k.id];l.load=false;var h=l.op.fclass;l.preid=d.nav.page.activeid||"";l.op=k;l.$page.removeAttr("style");l.$page.attr("data-type",k.type);l.$page.removeClass(h);l.$page.addClass(k.fclass);l.hasLoad=true}else{k.pageid=d.guid();var i='<div class="f-page '+k.fclass+'" data-type="'+k.type+'" id="'+k.pageid+'">';i+='<div class="f-page-header" >';i+='<div class="f-page-backbtn" > '+k.backbtn+" </div>";i+='<div class="f-page-title">'+k.title+"</div>";i+="</div>";i+='<div class="f-page-body"></div>';i+="</div>";a("body").append(i);l={$page:a("#"+k.pageid),preid:d.nav.page.activeid||"",op:k,load:false};d.nav.page.data[k.id]=l;d.nav.page.count++;b.bindEvent(l.$page)}d.nav.page.activeid=k.id;k.start&&k.start(l);switch(k.type){case"right":l.$page.animate({left:"0px"},200,function(){b.addPage(l);j()});break;case"bottom":l.$page.animate({top:"0px"},200,function(){b.addPage(l);j()});break;default:l.$page.fadeIn(function(){b.addPage(l);j()});break}},close:function(k,i){var j=k.op;if(j.bfdestroy){if(!j.bfdestroy(k,"backbtn")){i()}}d.nav.page.activeid="";if(k.preid){var h=d.nav.page.data[k.preid];h.load=false;h.$page.show();d.nav.page.activeid=k.preid;h=null}k.$page.css({"z-index":3});switch(j.type){case"right":k.$page.animate({left:"100%"},200,function(){b.removePage(k);i()});break;case"bottom":k.$page.animate({top:"100%"},200,function(){b.removePage(k);i()});break;default:k.$page.fadeOut(function(){b.removePage(k);i()});break}},bindEvent:function(h){h.find(".f-page-backbtn").on("tap",function(){var i=d.nav.page.activeid;var j=d.nav.page.data[i];if(!j.load){return false}g[f]={type:"close",op:j};f++;b.doQueue();return false})},addPage:function(i){if(i.preid!=""){var h=d.nav.page.data[i.preid];h.$page.hide()}i.$page.css({"z-index":1});i.op.end&&i.op.end(i);i.load=true},removePage:function(i){i.$page.remove();i.$page=null;a('[data-page="'+i.op.pageid+'"]').remove();i.op.destroy&&i.op.destroy(i);if(i.preid){var h=d.nav.page.data[i.preid];if(h.op.end){h.hasLoad=true;h.op.end(h)}h.load=true}d.nav.page.count--;d.nav.page.data[i.op.id]=null},remove:function(h){var i=d.nav.page.data[h];if(i!=null){i.$page.remove();i.$page=null;a('[data-page="'+i.op.pageid+'"]').remove();d.nav.page.count--;d.nav.page.data[i.op.id]=null;i=null}}};d.nav={page:{data:{},count:0,activeid:""},go:function(i){var h={id:"",title:"",type:"",backbtn:'<a href="#">返回</a>',fclass:"",start:false,end:false,bfdestroy:false,destroy:false};a.extend(h,i||{});g[f]={type:"open",op:i};f++;b.doQueue()},closeCurrent:function(){var h=d.nav.page.activeid;var i=d.nav.page.data[h];g[f]={type:"close",op:i};f++;b.doQueue()},close:function(h){b.remove(h)}}})(window.jQuery,window.fui);(function(a,b){b.layer={open:function(f){var e={title:"",backbtn:'<a href="#">返回</a>',open:false,close:false,cancel:false,};a.extend(e,f||{});e.layerid=b.guid();var d='            <div class="f-layer" id="'+e.layerid+'">                <div class="f-layer-closebtn" >关闭</div>                <div class="f-layer-cancelbtn" >取消</div>                <div class="f-layer-page" data-level="0" >                    <div class="f-layer-page-header" >                        <div class="f-layer-page-title">'+e.title+'</div>                    </div>                    <div class="f-layer-page-body"></div>                </div>                <div class="f-layer-mask" ></div>            </div>';a("body").append(d);var c=a("#"+e.layerid);c[0].fop=e;c.find(".f-layer-cancelbtn").on("tap",function(){var h=a(this);var g=h.parent();g.animate({top:"100%"},200,function(){var i=a(this)[0].fop;setTimeout(function(){var j=a("#"+i.layerid);j.remove();i.cancel&&i.cancel();j=null},100)})});c.find(".f-layer-closebtn").on("tap",function(){var h=a(this);var g=h.parent();var j=g[0].fop;var i=g.find('[data-level="'+j.level+'"]');g.find(".f-layer-closebtn").hide();g.find(".f-layer-page").show();i.animate({left:"100%"},200,function(){var k=a(this);var l=k.parent()[0].fop;l.level=0;setTimeout(function(){g.find(".f-layer-left-page").remove();l.close&&l.close(l.layerid,l.level,l);g=null},100)})});e.level=0;c.animate({top:"0px"},200,function(){var g=a(this)[0].fop;setTimeout(function(){var h=a("#"+g.layerid+' [data-level="'+g.level+'"] .f-layer-page-body');g.open&&g.open(h,g.layerid,g.level,g)},100)});c=null},iopen:function(h,i){var c=a("#"+h);var d=c.find(".f-layer-mask");d.show();var g=c[0].fop;g.level=g.level+1;var f='            <div class="f-layer-left-page" data-level="'+g.level+'" >                <div class="f-layer-page-header" >                    <div class="f-layer-page-backbtn" >'+g.backbtn+'</div>                    <div class="f-layer-page-title">'+i+'</div>                </div>                <div class="f-layer-page-body"></div>            </div>';c.append(f);var e=c.find('[data-level="'+g.level+'"]');e.find(".f-layer-page-backbtn").on("tap",function(){var j=a(this);var l=j.parents(".f-layer-left-page");var k=l.parent();var m=k[0].fop;m.level=m.level-1;if(m.level<2){k.find(".f-layer-closebtn").hide()}k.find('[data-level="'+m.level+'"]').show();l.css({"z-index":5});l.animate({left:"100%"},200,function(){var n=a(this);var o=n.parent()[0].fop;setTimeout(function(){n.remove();o.close&&o.close(o.layerid,o.level,o)},100)})});e.animate({left:"0px"},200,function(){var k=a(this);k.css({"z-index":4});var j=k.parent();var l=j[0].fop;if(l.level>=2){j.find(".f-layer-closebtn").show()}j.find('[data-level="'+(l.level-1)+'"]').hide();setTimeout(function(){var m=k.find(".f-layer-page-body");l.open&&l.open(m,l.layerid,l.level,l)},100);d.hide()});c=null},close:function(c){a("#"+c+" .f-layer-cancelbtn").trigger("click")},iclose:function(c){a("#"+c+" .f-layer-closebtn").trigger("click")},closeLayer:function(d){var c=a("#"+d);var e=c[0].fop.level;c.find('[data-level="'+e+'"] .f-layer-page-backbtn').trigger("click")}}})(window.jQuery,window.fui);(function(a,c){var e=navigator.platform.toLowerCase();var f=navigator.userAgent.toLowerCase();var d=(f.indexOf("iphone")>-1||f.indexOf("ipad")>-1||f.indexOf("ipod")>-1)&&(e.indexOf("iphone")>-1||e.indexOf("ipad")>-1||e.indexOf("ipod")>-1);var b={init:function(g,i){var h='<div class="f-pciker-rule"></div><ul class="f-pciker-list"></ul>';g.addClass("f-picker");g.html(h);b.initParams(g);b.initData(g,i);b.bindEvent(g)},initParams:function(g){var h=g[0];h.list=g.find("ul")[0];h.height=h.offsetHeight;h.r=h.height/2-10;h.d=h.r*2;h.itemHeight=40;h.itemAngle=parseInt(b.calcAngle(h,h.itemHeight*0.8));h.hightlightRange=h.itemAngle/2;h.visibleRange=90;h.beginAngle=0;h.beginExceed=h.beginAngle-30;h.list.angle=h.beginAngle;h.lastMoveTime=0;h.lastMoveStart=0;h.stopInertiaMove=false;h.lastAngle=0;h.startY=null;h.isPicking=false;h.selected=null;h.preselected=null;h.list.style.webkitTransition="150ms ease-out";if(d){h.list.style.webkitTransformOrigin="center center "+h.r+"px"}},initData:function(i,k){var g=i.find("ul");g.html("");var l=i[0];l.items=[];var j="";k.data=k.data||[];a.each(k.data,function(m,o){var n={id:m,text:o[k.text],value:o[k.value],angle:l.itemAngle*m,obj:o};l.endAngle=n.angle;l.items.push(n);if(m<=20){j+='<li data-index="'+m+'" style="transform-origin: center center -'+l.r+"px; transform: translateZ("+l.r+"px) rotateX(-"+n.angle+'deg);">'+n.text+"</li>"}});l.beginindex=0;l.endindex=20;l.endExceed=l.endAngle+30;g.html(j);b.setAngle(l,l.beginAngle);if(!!l.selected){var h=a(l);h.trigger("change");if(!!k.change){k.change(l.selected.obj,h)}}},bindEvent:function(g){var h=g[0];h.addEventListener(c.event.start,function(i){var j=a(this)[0];j.isPicking=true;i.preventDefault();j.list.style.webkitTransition="";j.startY=(i.changedTouches?i.changedTouches[0]:i).pageY;j.lastAngle=j.list.angle;b.updateInertiaParams(j,i,true)},false);h.addEventListener(c.event.end,function(i){var j=a(this)[0];j.isPicking=false;i.preventDefault();b.startInertiaScroll(j,i)},false);h.addEventListener(c.event.cancel,function(i){var j=a(this)[0];j.isPicking=false;i.preventDefault();b.startInertiaScroll(j,i)},false);h.addEventListener(c.event.move,function(l){var n=a(this)[0];if(!n.isPicking){return}l.preventDefault();var k=(l.changedTouches?l.changedTouches[0]:l).pageY;var j=k-n.startY;var i=b.calcAngle(n,j);var m=j>0?n.lastAngle-i:n.lastAngle+i;if(m>n.endExceed){m=n.endExceed}if(m<n.beginExceed){m=n.beginExceed}b.setAngle(n,m);b.updateInertiaParams(n,l)},false)},updateInertiaParams:function(k,g,h){var j=g.changedTouches?g.changedTouches[0]:g;if(h){k.lastMoveStart=j.pageY;k.lastMoveTime=g.timeStamp||Date.now();k.startAngle=k.list.angle}else{var i=g.timeStamp||Date.now();if(i-k.lastMoveTime>300){k.lastMoveTime=i;k.lastMoveStart=j.pageY}}k.stopInertiaMove=true},startInertiaScroll:function(o,l){var n=l.changedTouches?l.changedTouches[0]:l;var m=l.timeStamp||Date.now();var r=(n.pageY-o.lastMoveStart)/(m-o.lastMoveTime);var h=r>0?-1:1;var g=h*0.0006*-1;var k=Math.abs(r/g);var i=r*k/2;var q=o.list.angle;var j=b.calcAngle(o,i)*h;var p=j;if(q+j<o.beginExceed){j=o.beginExceed-q;k=k*(j/p)*0.6}if(q+j>o.endExceed){j=o.endExceed-q;k=k*(j/p)*0.6}if(j==0){b.endScroll(o);return}b.scrollDistAngle(o,m,q,j,k)},scrollDistAngle:function(j,i,k,g,h){j.stopInertiaMove=false;(function(p,q,l,m){var n=13;var r=m/n;var s=0;(function o(){if(j.stopInertiaMove){return}var t=b.quartEaseOut(s,q,l,r);b.setAngle(j,t);s++;if(s>r-1||t<j.beginExceed||t>j.endExceed){b.endScroll(j);return}setTimeout(o,n)})()})(i,k,g,h)},endScroll:function(j){if(j.list.angle<j.beginAngle){j.list.style.webkitTransition="150ms ease-out";b.setAngle(j,j.beginAngle)}else{if(j.list.angle>j.endAngle){j.list.style.webkitTransition="150ms ease-out";b.setAngle(j,j.endAngle)}else{var h=parseInt((j.list.angle/j.itemAngle).toFixed(0));j.list.style.webkitTransition="100ms ease-out";b.setAngle(j,j.itemAngle*h)}}if(j.preselected!=j.selected){var g=a(j);var i=j.fop;g.trigger("change");if(!!i.change){i.change(j.selected.obj,g)}}},calcElementItemVisibility:function(o,l){var h=a(o);var g=h.find("ul");var k="";var q=0;var p=o.items.length;a.each(o.items,function(i,s){var r=Math.abs(s.angle-l);if(r<o.hightlightRange){o.selected=s;if(!s.hightlight||!g.find('li[data-index="'+i+'"]').hasClass("highlight")){s.hightlight=true;g.find('li[data-index="'+i+'"]').addClass("highlight")}}else{if(r<o.visibleRange){if(!s.visible||!g.find('li[data-index="'+i+'"]').hasClass("visible")){s.visible=true;g.find('li[data-index="'+i+'"]').addClass("visible")}if(!!s.hightlight||g.find('li[data-index="'+i+'"]').hasClass("highlight")){s.hightlight=false;g.find('li[data-index="'+i+'"]').removeClass("highlight")}if(p>i){p=i}if(q<i){q=i}}else{if(!!s.visible||g.find('li[data-index="'+i+'"]').hasClass("visible")){s.visible=false;g.find('li[data-index="'+i+'"]').removeClass("visible")}if(!!s.hightlight||g.find('li[data-index="'+i+'"]').hasClass("highlight")){s.hightlight=false;g.find('li[data-index="'+i+'"]').removeClass("highlight")}}}});if(o.items.length>21){if(q>=o.endindex){var n=o.endindex+11;for(var m=o.endindex+1;m<n;m++){if(!!o.items[m]){var j="";if(!!o.items[m].visible){j='class="visible"'}g.append("<li "+j+' data-index="'+m+'"  style="transform-origin: center center -'+o.r+"px; transform: translateZ("+o.r+"px) rotateX(-"+o.items[m].angle+'deg);">'+o.items[m].text+"</li>");g.find('li[data-index="'+(m-21)+'"]').remove();o.endindex++;o.beginindex++}}}else{if(p<=o.beginindex){var n=o.beginindex-11;for(var m=o.beginindex-1;m>n;m--){if(!!o.items[m]){var j="";if(!!o.items[m].visible){j='class="visible"'}g.prepend("<li "+j+' data-index="'+m+'"  style="transform-origin: center center -'+o.r+"px; transform: translateZ("+o.r+"px) rotateX(-"+o.items[m].angle+'deg);">'+o.items[m].text+"</li>");g.find('li[data-index="'+(m+21)+'"]').remove();o.endindex--;o.beginindex--}}}}}},setAngle:function(h,g){h.list.angle=g;h.list.style.webkitTransform="perspective(1000px) rotateY(0deg) rotateX("+g+"deg)";b.calcElementItemVisibility(h,g)},calcAngle:function(m,j){var g=parseFloat(m.r);var i=parseFloat(m.r);j=Math.abs(j);var l=parseInt(j/m.d)*180;j=j%m.d;var k=(g*g+i*i-j*j)/(2*g*i);var h=l+b.rad2deg(Math.acos(k));return h},rad2deg:function(g){return g/(Math.PI/180)},quartEaseOut:function(j,g,h,i){return -h*((j=j/i-1)*j*j*j-1)+g}};a.fn.fpicker=function(i){var h={data:[],value:"value",text:"text",change:null};var g=a(this);if(!!g[0].fop){return g}a.extend(h,i||{});g[0].fop=h;b.init(g,h);return g};a.fn.fpickerSetData=function(h){var g=a(this);if(!g[0].fop){return g}g[0].fop.data=h;b.initParams(g);b.initData(g,g[0].fop);return g};a.fn.fpickerSet=function(i){var g=a(this);var h=g[0];if(!h.fop){return g}h._value=i;a.each(h.items,function(l,n){if(n.value==i){b.setAngle(h,n.angle);if(h.items.length>21){var j=g.find("ul");j.html("");h.beginindex=n.id-10;h.endindex=n.id+10;for(var m=h.beginindex;m<h.endindex+1;m++){if(!!h.items[m]){var k="";if(!!h.items[m].visible){k='class="visible"'}if(!!h.items[m].hightlight){k='class="highlight"'}j.append("<li "+k+' data-index="'+m+'"  style="transform-origin: center center -'+h.r+"px; transform: translateZ("+h.r+"px) rotateX(-"+h.items[m].angle+'deg);">'+h.items[m].text+"</li>")}}if(!!h.selected){g.trigger("change");if(!!h.fop.change){h.fop.change(h.selected.obj,g)}}}return false}})};a.fn.fpickerGet=function(){var g=a(this);return g[0].selected.value||""};a.fn.fpickerGetObj=function(){var g=a(this);return g[0].selected}})(window.jQuery,window.fui);(function(a,c){var b={init:function(d,f){f.id=c.guid();var e='<div class="f-poppicker f-pop" id="pop_'+f.id+'"  data-page="'+c.pageid()+'"  >';e+='<div class="f-poppicker-header" >';e+='<div class="f-poppicker-cancel">取消</div>';e+='<div class="f-poppicker-ok">确定</div>';e+="</div>";e+='<div class="f-poppicker-body" ></div>';e+="</div>";a("body").append(e);c.createMask();b.initPicker(f);b.bindEvent(d,f)},bindEvent:function(e,f){e.on("tap",function(){var h=a(this);if(h.attr("readonly")||h.parents(".lr-form-row").attr("readonly")){return false}var j=h[0];var i=j.fop;if(!!i.callback){i.callback()}var g=a("#pop_"+i.id);if(!g.hasClass("active")){g.addClass("active");c.showMask()}setTimeout(function(){if(i.value!=undefined&&i.value!=""&&i.value!=null){var k=i.value.split(",");a.each(k,function(m,n){var l=a("#picker_"+i.id+"_"+m);l.fpickerSet(n)})}},300);return false});var d=a("#pop_"+f.id);d.find(".f-poppicker-cancel").on("tap",function(){var g=a(this).parents(".f-poppicker");g.removeClass("active");c.hideMask();return false});d.find(".f-poppicker-ok").on("tap",{$self:e,fop:f},function(l){var h=a(this).parents(".f-poppicker");h.removeClass("active");c.hideMask();l=l||window.event;var m=l.data.fop;var j=l.data.$self;var p=[];var q=[];var k=[];for(var n=0;n<m.level;n++){var g=a("#picker_"+m.id+"_"+n);var o=g.fpickerGetObj();p.push(o.text);q.push(o.value);k.push(o)}if(m.value!=String(q)){m.value=String(q);m.text=String(p);j.trigger("change");if(!!m.change){m.change(m.value,m.text,k,j)}}return false})},initPicker:function(g){var e=a("#pop_"+g.id+" .f-poppicker-body");for(var f=0;f<g.level;f++){var d=a('<div id="picker_'+g.id+"_"+f+'"  data-level="'+f+'"  ></div>');d.css("width",(100/g.level+"%"));e.append(d);d.fpicker({data:[],value:g.ivalue,text:g.itext,change:function(k,h){var j=parseInt(h.attr("data-level"))+1;if(g.level>1&&j<g.level){var i=a("#picker_"+g.id+"_"+j);i.fpickerSetData(k.children)}}})}a("#picker_"+g.id+"_0").fpickerSetData(g.data)}};a.fn.fpoppicker=function(f){var e={data:[],level:1,change:false,ivalue:"value",itext:"text"};var d=a(this);if(!!d[0].fop){return d}a.extend(e,f||{});d[0].fop=e;b.init(d,e);return d};a.fn.fpoppickerSet=function(h){var d=a(this);var f=d[0].fop;if(h!=undefined&&h!=""&&h!=null){var i=h.split(",");var g=[];var e=[];a.each(i,function(k,m){var j=a("#picker_"+f.id+"_"+k);j.fpickerSet(m);var l=j.fpickerGetObj();if(l){g.push(l.text||"");e.push(l)}});f.value=h;f.text=String(g);d.trigger("change");if(!!f.change){f.change(f.value,f.text,e,d)}}};a.fn.fpoppickerSetData=function(e){var d=a(this);if(d.length>0){var f=d[0].fop;if(f){a("#picker_"+f.id+"_0").fpickerSetData(e)}}d=null}})(window.jQuery,window.fui);(function(a,c){var b={init:function(e,h){h.id=c.guid();var g='<div class="f-dtpicker f-pop" id="dt_'+h.id+'" data-type="'+h.type+'"   data-page="'+c.pageid()+'"  >';g+='<div class="f-dtpicker-header" >';g+='<div class="f-dtpicker-cancel">取消</div>';g+='<div class="f-dtpicker-ok">确定</div>';g+="</div>";g+='<div class="f-dtpicker-title">';g+='<h5 data-id="title-y">'+h.label[0]+"</h5>";g+='<h5 data-id="title-m">'+h.label[1]+"</h5>";g+='<h5 data-id="title-d">'+h.label[2]+"</h5>";g+='<h5 data-id="title-h">'+h.label[3]+"</h5>";g+='<h5 data-id="title-i">'+h.label[4]+"</h5>";g+="</div>";g+='<div class="f-dtpicker-body" ></div>';g+="</div>";a("body").append(g);c.createMask();switch(h.type){case"datetime":var f=b.initYear(h);var d=b.initMonth(h);b.initDay(h,f,d);b.initHour(h);b.initMinute(h);break;case"date":var f=b.initYear(h);var d=b.initMonth(h);b.initDay(h,f,d);break;case"time":b.initHour(h);b.initMinute(h);break;case"month":b.initYear(h);b.initMonth(h);break;default:var f=b.initYear(h);var d=b.initMonth(h);b.initDay(h,f,d);b.initHour(h);b.initMinute(h);break}b.bindEvent(e,h)},bindEvent:function(e,f){e.on("tap",function(){var h=a(this);if(h.attr("readonly")||h.parents(".lr-form-row").attr("readonly")){return false}var j=h[0];var i=j.fop;if(!!i.callback){i.callback()}var g=a("#dt_"+i.id);if(!g.hasClass("active")){g.addClass("active");c.showMask()}setTimeout(function(){if(i.value!=undefined&&i.value!=""&&i.value!=null){b.setValue(i,i.value)}},300);return false});var d=a("#dt_"+f.id);d.find(".f-dtpicker-cancel").on("tap",function(){var g=a(this).parents(".f-dtpicker");g.removeClass("active");c.hideMask();return false});d.find(".f-dtpicker-ok").on("tap",{$self:e,fop:f},function(i){var g=a(this).parents(".f-dtpicker");g.removeClass("active");c.hideMask();var j=i.data.fop;var h=i.data.$self;var l="";switch(j.type){case"datetime":var k=b.getfYear(j)+"-"+b.getfMonth(j)+"-"+b.getfDay(j)+" "+b.getfHour(j)+":"+b.getfMinute(j);l=c.date.format(k,j.format||"yyyy-MM-dd hh:mm");break;case"date":var k=b.getfYear(j)+"-"+b.getfMonth(j)+"-"+b.getfDay(j);l=c.date.format(k,j.format||"yyyy-MM-dd");break;case"time":var k="2017-12-18 "+b.getfHour(j)+":"+b.getfMinute(j);l=c.date.format(k,j.format||"hh:mm");break;case"month":var k=b.getfYear(j)+"-"+b.getfMonth(j)+"-01";l=c.date.format(k,j.format||"yyyy-MM");break}if(j.value!=l){j.value=l;h.trigger("change");if(!!j.change){j.change(j.value,h)}}return false})},initYear:function(l){var h=[];var g=new Date();var m=g.getFullYear();for(var j=-50;j<51;j++){var f=m+j;var k={text:f,value:f};h.push(k)}var e=a("#dt_"+l.id+" .f-dtpicker-body");var d=a('<div id="picker_'+l.id+'_y" class="f-dt-picker f-dt-y" ></div>');e.append(d);d.fpicker({data:h}).fpickerSet(m);return d},initMonth:function(h){var f=new Date();var g=f.getMonth()+1;var e=a("#dt_"+h.id+" .f-dtpicker-body");var d=a('<div id="picker_'+h.id+'_m" class="f-dt-picker f-dt-m" ></div>');e.append(d);d.fpicker({data:[{text:"01",value:1},{text:"02",value:2},{text:"03",value:3},{text:"04",value:4},{text:"05",value:5},{text:"06",value:6},{text:"07",value:7},{text:"08",value:8},{text:"09",value:9},{text:"10",value:10},{text:"11",value:11},{text:"12",value:12}]}).fpickerSet(g);return d},initDay:function(k,g,d){var h=new Date();var i=h.getDate();var j=d.fpickerGet();var l=g.fpickerGet();var f=a("#dt_"+k.id+" .f-dtpicker-body");var e=a('<div id="picker_'+k.id+'_d" class="f-dt-picker f-dt-d" ></div>');f.append(e);e.fpicker({data:b.getDayData(l,j)}).fpickerSet(i);g[0].fop.change=function(t,m){var r=t.value;var q=d.fpickerGet();var o=e.fpickerGet();var p=e[0].fop.data.length;var s=b.getDayNum(r,q);if(p!=s){if(o>s){o=s}var n=b.getDayData(r,q);e.fpickerSetData(n).fpickerSet(o)}};d[0].fop.change=function(t,m){var q=t.value;var r=g.fpickerGet();var o=e.fpickerGet();var p=e[0].fop.data.length;var s=b.getDayNum(r,q);if(p!=s){if(o>s){o=s}var n=b.getDayData(r,q);e.fpickerSetData(n).fpickerSet(o)}}},initHour:function(i){var g=[{text:"00",value:0},{text:"01",value:1},{text:"02",value:2},{text:"03",value:3},{text:"04",value:4},{text:"05",value:5},{text:"06",value:6},{text:"07",value:7},{text:"08",value:8},{text:"09",value:9},{text:"10",value:10},{text:"11",value:11},{text:"12",value:12},{text:"13",value:13},{text:"14",value:14},{text:"15",value:15},{text:"16",value:16},{text:"17",value:17},{text:"18",value:18},{text:"19",value:19},{text:"20",value:20},{text:"21",value:21},{text:"22",value:22},{text:"23",value:23},];var f=new Date();var h=f.getHours();var e=a("#dt_"+i.id+" .f-dtpicker-body");var d=a('<div id="picker_'+i.id+'_h" class="f-dt-picker f-dt-h" ></div>');e.append(d);d.fpicker({data:g}).fpickerSet(h)},initMinute:function(i){var g=[{text:"00",value:0},{text:"01",value:1},{text:"02",value:2},{text:"03",value:3},{text:"04",value:4},{text:"05",value:5},{text:"06",value:6},{text:"07",value:7},{text:"08",value:8},{text:"09",value:9},{text:"10",value:10},{text:"11",value:11},{text:"12",value:12},{text:"13",value:13},{text:"14",value:14},{text:"15",value:15},{text:"16",value:16},{text:"17",value:17},{text:"18",value:18},{text:"19",value:19},{text:"20",value:20},{text:"21",value:21},{text:"22",value:22},{text:"23",value:23},{text:"24",value:24},{text:"25",value:25},{text:"26",value:26},{text:"27",value:27},{text:"28",value:28},{text:"29",value:29},{text:"30",value:30},{text:"31",value:31},{text:"32",value:32},{text:"33",value:33},{text:"34",value:34},{text:"35",value:35},{text:"36",value:36},{text:"37",value:37},{text:"38",value:38},{text:"39",value:39},{text:"40",value:40},{text:"41",value:41},{text:"42",value:42},{text:"43",value:43},{text:"44",value:44},{text:"45",value:45},{text:"46",value:46},{text:"47",value:47},{text:"48",value:48},{text:"49",value:49},{text:"50",value:50},{text:"51",value:51},{text:"52",value:52},{text:"53",value:53},{text:"54",value:54},{text:"55",value:55},{text:"56",value:56},{text:"57",value:57},{text:"58",value:58},{text:"59",value:59}];var f=new Date();var h=f.getMinutes();var e=a("#dt_"+i.id+" .f-dtpicker-body");var d=a('<div id="picker_'+i.id+'_i" class="f-dt-picker f-dt-i" ></div>');e.append(d);d.fpicker({data:g}).fpickerSet(h)},getDayData:function(j,h){var f=[];for(var g=1;g<=b.getDayNum(j,h);g++){var e="";if(g<10){e="0"+g}else{e=""+g}var d={text:e,value:g};f.push(d)}return f},getDayNum:function(e,d){if([1,3,5,7,8,10,12].indexOf(d)>-1){return 31}else{if([4,6,9,11].indexOf(d)>-1){return 30}else{if(c.date.isLeapYear(e)){return 29}else{return 28}}}},getfYear:function(e){var d=a("#picker_"+e.id+"_y");return d.fpickerGet()},getfMonth:function(e){var d=a("#picker_"+e.id+"_m");return d.fpickerGetObj().text},getfDay:function(e){var d=a("#picker_"+e.id+"_d");return d.fpickerGetObj().text},getfHour:function(e){var d=a("#picker_"+e.id+"_h");return d.fpickerGetObj().text},getfMinute:function(e){var d=a("#picker_"+e.id+"_i");return d.fpickerGetObj().text},setValue:function(k,l){if(k.type=="time"){l="2017-12-08 "+l}var j=c.date.parse(l);var i=a("#picker_"+k.id+"_y");if(i.length>0){i.fpickerSet(j.getFullYear())}var h=a("#picker_"+k.id+"_m");if(h.length>0){h.fpickerSet(j.getMonth()+1)}var e=a("#picker_"+k.id+"_d");if(e.length>0){e.fpickerSet(j.getDate())}var f=a("#picker_"+k.id+"_h");if(f.length>0){f.fpickerSet(j.getHours())}var g=a("#picker_"+k.id+"_i");if(g.length>0){g.fpickerSet(j.getMinutes())}}};a.fn.fdtpicker=function(f){var e={type:"datetime",label:["年","月","日","时","分"],format:false,change:false};var d=a(this);if(!!d[0].fop){return d}a.extend(e,f||{});d[0].fop=e;b.init(d,e);return d}})(window.jQuery,window.fui);(function(a){var b={init:function(c){c.addClass("f-switch");c.html('<div class="f-switch-handle"></div>');c.on("tap",function(){var d=a(this);if(d.attr("readonly")||d.parents(".lr-form-row").attr("readonly")){return false}if(d.hasClass("f-active")){d.removeClass("f-active")}else{d.addClass("f-active")}d.trigger("change");return false})}};a.fn.fswitch=function(){a(this).each(function(){var c=a(this);b.init(c)});return a(this)};a.fn.fswitchGet=function(){var c=a(this);var d=0;if(c.hasClass("f-active")){d=1}return d};a.fn.fswitchSet=function(d){var c=a(this);if(d!=1&&d!="1"){c.removeClass("f-active")}else{if(!c.hasClass("f-active")){c.addClass("f-active")}}}})(window.jQuery);(function(a,c){var b={init:function(h){if(!h.id){alert("设置下id信息!");return false}var d=a("#"+h.id);if(d.length==0){var e='<div class="f-actionsheet f-pop" id="'+h.id+'" data-page="'+c.pageid()+'"  ></div>';c.createMask();d=a(e);a("body").append(d);var i={};var f=c.guid();a.each(h.data,function(j,n){var k=a.extend({group:f,text:"按钮"+j,},n);i[k.group]=i[k.group]||a('<ul class="f-table"></ul>');var m="";if(!!k.mark){m='style="color:red;"'}var l='<li class="f-table-cell"><a data-event="'+j+'" '+m+' href="#">'+k.text+"</a></li>";i[k.group].append(l)});for(var g in i){d.append(i[g])}d.append('<ul class="f-table"><li class="f-table-cell"><a data-event="cancel" href="#">取消</a></li></ul>');d.on("tap","a",{op:h},function(k){var j=a(this);var n=k.data.op;var m=j.attr("data-event");var l=null;if(m=="cancel"){l=n.cancel}else{l=n.data[m].event}c.hideMask();a("#"+n.id).removeClass("active");if(!!l){setTimeout(function(){l()},300)}return false})}c.showMask();d.addClass("active")}};c.actionsheet=function(e){var d={id:"",data:[],cancel:false};a.extend(d,e||{});b.init(d)}})(window.jQuery,window.fui);(function(a,c){var b={init:function(g){var e=a("body");if(g.type!="toast"){if(a(".f-dialog").length>0){a(".f-dialog").remove();a(".f-dialog-mask").remove();return false}var f='<div class="f-dialog"></div><div class="f-dialog-mask"></div>';e.append(f);b[g.type](g);b.bindEvent(g);a(".f-dialog-mask").fadeIn();a(".f-dialog").show("fast",function(){a(this).addClass("active")})}else{var d=a('<div class="f-toast-container" ><div class="f-toast-message" >'+g.msg+"</div></div>");e.append(d);d.fadeIn(function(){var h=d;setTimeout(function(){h.fadeOut(function(){setTimeout(function(){h.remove();h=null},100)})},g.timeout);d.on("click",function(){var i=a(this);i.fadeOut(function(){i.remove();setTimeout(function(){i.remove();i=null},100)})});d=null})}},bindEvent:function(d){a(".f-dialog").on("tap",".f-dialog-btn",{op:d},function(h){var i=h.data.op;var f=a(this).attr("data-index");var g=a(".f-dialog input").val();a(".f-dialog-mask").fadeOut(function(){a(".f-dialog-mask").remove()});a(".f-dialog").fadeOut(function(){a(".f-dialog").remove()});if(!!i.callback){setTimeout(function(){i.callback(f,g)},300)}})},warning:function(g){var f=a(".f-dialog");var d=a('<div class="f-dialog-body" ><div class="f-dialog-title" >'+g.title+'</div><div class="f-dialog-text">'+g.msg+"</div></div>");var e=a('<div class="f-dialog-btns"><span data-index="0" class="f-dialog-btn f-dialog-btn-bold">'+g.btn+"</span></div>");f.append(d);f.append(e)},confirm:function(h){var f=a(".f-dialog");var d=a('<div class="f-dialog-body" ><div class="f-dialog-title" >'+h.title+'</div><div class="f-dialog-text">'+h.msg+"</div></div>");var e=a('<div class="f-dialog-btns"></div>');var g="";a.each(h.btns,function(i,j){g+='<span data-index="'+i+'" class="f-dialog-btn">'+j+"</span>"});e.html(g);f.append(d);f.append(e)},prompt:function(h){var f=a(".f-dialog");var d=a('<div class="f-dialog-body" ><div class="f-dialog-title" >'+h.title+'</div><div class="f-dialog-text">'+h.msg+'</div>            <div class="f-dialog-input" ><input type="text" placeholder="'+(h.input||"")+'"></div></div>');var e=a('<div class="f-dialog-btns"></div>');var g="";a.each(h.btns,function(i,j){g+='<span data-index="'+i+'" class="f-dialog-btn">'+j+"</span>"});e.html(g);f.append(d);f.append(e);f.find("input").focus()}};c.dialog=function(e){var d={type:"toast",title:"提示",msg:"",btn:"确定",timeout:2000,btns:["否","是"],callback:false};a.extend(d,e||{});b.init(d)};c.loading=function(f,g){if(f){var e="";if(!!g){e='<div class="f-load-msg" >'+(g||"")+"</div>"}var d=a('<div class="f-load-mask"></div><div class="f-load-container" ><div class="f-load-body" ><div style="margin-right:0px;" class="f-pull-loading f-spinner" ></div>            '+e+"</div></div>");a("body").append(d);d.fadeIn()}else{a(".f-load-mask").fadeOut(function(){a(this).remove()});a(".f-load-container").fadeOut(function(){a(this).remove()})}}})(window.jQuery,window.fui);(function(a,k,i){var e="f-scroll-wrapper";var d="f-scroll";var f="f-scrollbar";var c="f-scrollbar-indicator";var h=f+"-vertical";var g=f+"-horizontal";var b="f-active";var j={quadratic:{style:"cubic-bezier(0.25, 0.46, 0.45, 0.94)",fn:function(n){return n*(2-n)}},circular:{style:"cubic-bezier(0.1, 0.57, 0.1, 1)",fn:function(n){return Math.sqrt(1-(--n*n))}},outCirc:{style:"cubic-bezier(0.075, 0.82, 0.165, 1)"},outCubic:{style:"cubic-bezier(0.165, 0.84, 0.44, 1)"}};var m=k.Class.extend({init:function(n,p){var o=a('<div class="'+d+'" ></div>');o.html(n.children());n.addClass(e);n.html(o);this.element=n[0];this.wrapper=this.element;this.scroller=this.wrapper.children[0];this.scrollerStyle=this.scroller&&this.scroller.style;this.stopped=false;this.options=a.extend(true,{scrollY:true,scrollX:false,startX:0,startY:0,indicators:true,stopPropagation:false,hardwareAccelerated:true,fixedBadAndorid:false,preventDefaultException:{tagName:/^(INPUT|TEXTAREA|BUTTON|SELECT|VIDEO)$/},momentum:true,snapX:0.5,snap:false,bounce:true,bounceTime:500,bounceEasing:j.outCirc,scrollTime:500,scrollEasing:j.outCubic,directionLockThreshold:5,},p);this.x=0;this.y=0;this.translateZ=this.options.hardwareAccelerated?" translateZ(0)":"";this._init();if(this.scroller){this.refresh();this.scrollTo(this.options.startX,this.options.startY)}},_init:function(){this._initIndicators();this._initEvent()},_initIndicators:function(){var q=this;q.indicators=[];if(!this.options.indicators){return}var p=[],o;if(q.options.scrollY){o={el:this._createScrollBar(h),listenX:false};this.wrapper.appendChild(o.el);p.push(o)}if(this.options.scrollX){o={el:this._createScrollBar(g),listenY:false};this.wrapper.appendChild(o.el);p.push(o)}for(var n=p.length;n--;){this.indicators.push(new l(this,p[n]))}},_initSnap:function(){this.currentPage={};this.pages=[];var y=this.snaps;var q=y.length;var r=0;var s=-1;var A=0;var p=0;var v=0;var z=0;for(var o=0;o<q;o++){var w=y[o];var t=w.offsetLeft;var u=w.offsetWidth;if(o===0||t<=y[o-1].offsetLeft){r=0;s++}if(!this.pages[r]){this.pages[r]=[]}A=this._getSnapX(t);z=Math.round((u)*this.options.snapX);p=A-z;v=A-u+z;this.pages[r][s]={x:A,leftX:p,rightX:v,pageX:r,element:w};if(w.classList.contains(b)){this.currentPage=this.pages[r][0]}if(A>=this.maxScrollX){r++}}this.options.startX=this.currentPage.x||0},_getSnapX:function(n){return Math.max(Math.min(0,-n+(this.wrapperWidth/2)),this.maxScrollX)},_gotoPage:function(o){this.currentPage=this.pages[Math.min(o,this.pages.length-1)][0];for(var n=0,p=this.snaps.length;n<p;n++){if(n===o){this.snaps[n].classList.add(b)}else{this.snaps[n].classList.remove(b)}}this.scrollTo(this.currentPage.x,0,this.options.scrollTime)},_nearestSnap:function(q){if(!this.pages.length){return{x:0,pageX:0}}var n=0;var o=this.pages.length;if(q>0){q=0}else{if(q<this.maxScrollX){q=this.maxScrollX}}for(;n<o;n++){var p=this.direction==="left"?this.pages[n][0].leftX:this.pages[n][0].rightX;if(q>=p){return this.pages[n][0]}}return{x:0,pageX:0}},_initEvent:function(o){var n=o?"removeEventListener":"addEventListener";window[n]("orientationchange",this);window[n]("resize",this);this.scroller[n]("webkitTransitionEnd",this);this.wrapper[n](k.event.start,this);this.wrapper[n](k.event.cancel,this);this.wrapper[n](k.event.end,this);this.wrapper[n]("drag",this);this.wrapper[n]("dragend",this);this.wrapper[n]("flick",this);this.wrapper[n]("scrollend",this);if(this.options.scrollX){this.wrapper[n]("swiperight",this)}this.wrapper[n]("scrollstart",this);this.wrapper[n]("refresh",this)},_handleIndicatorScrollend:function(){this.indicators.map(function(n){n.fade()})},_handleIndicatorScrollstart:function(){this.indicators.map(function(n){n.fade(1)})},_handleIndicatorRefresh:function(){this.indicators.map(function(n){n.refresh()})},handleEvent:function(n){if(this.stopped){this.resetPosition();return}switch(n.type){case k.event.start:this._start(n);break;case"drag":this.options.stopPropagation&&n.stopPropagation();this._drag(n);break;case"dragend":case"flick":this.options.stopPropagation&&n.stopPropagation();this._flick(n);break;case k.event.cancel:case k.event.end:this._end(n);break;case"webkitTransitionEnd":this.transitionTimer&&this.transitionTimer.cancel();this._transitionEnd(n);break;case"scrollstart":this._handleIndicatorScrollstart(n);break;case"scrollend":this._handleIndicatorScrollend(n);this._scrollend(n);n.stopPropagation();break;case"orientationchange":case"resize":this._resize();break;case"swiperight":n.stopPropagation();break;case"refresh":this._handleIndicatorRefresh(n);break}},_start:function(n){this.moved=this.needReset=false;this._transitionTime();if(this.isInTransition){this.needReset=true;this.isInTransition=false;var o=k.parseTranslateMatrix(k.getStyles(this.scroller,"webkitTransform"));this.setTranslate(Math.round(o.x),Math.round(o.y));k.trigger(this.scroller,"scrollend",this);n.preventDefault()}this.reLayout();k.trigger(this.scroller,"beforescrollstart",this)},_getDirectionByAngle:function(n){if(n<-80&&n>-100){return"up"}else{if(n>=80&&n<100){return"down"}else{if(n>=170||n<=-170){return"left"}else{if(n>=-35&&n<=10){return"right"}}}}return null},_drag:function(t){var r=t.detail;var u=false;var v=false;var s=this._getDirectionByAngle(r.angle);if(r.direction==="left"||r.direction==="right"){if(this.options.scrollX){u=true;if(!this.moved){k.gestures.session.lockDirection=true;k.gestures.session.startDirection=r.direction}}else{if(this.options.scrollY&&!this.moved){v=true}}}else{if(r.direction==="up"||r.direction==="down"){if(this.options.scrollY){u=true;if(!this.moved){k.gestures.session.lockDirection=true;k.gestures.session.startDirection=r.direction}}else{if(this.options.scrollX&&!this.moved){v=true}}}else{v=true}}if(this.moved||u){t.stopPropagation();r.gesture&&r.gesture.preventDefault()}if(v){return}if(!this.moved){k.trigger(this.scroller,"scrollstart",this)}else{t.stopPropagation()}var p=0;var q=0;if(!this.moved){p=r.deltaX;q=r.deltaY}else{p=r.deltaX-k.gestures.session.prevTouch.deltaX;q=r.deltaY-k.gestures.session.prevTouch.deltaY}var n=Math.abs(r.deltaX);var o=Math.abs(r.deltaY);if(n>o+this.options.directionLockThreshold){q=0}else{if(o>=n+this.options.directionLockThreshold){p=0}}p=this.hasHorizontalScroll?p:0;q=this.hasVerticalScroll?q:0;var w=this.x+p;var x=this.y+q;if(w>0||w<this.maxScrollX){w=this.options.bounce?this.x+p/3:w>0?0:this.maxScrollX}if(x>0||x<this.maxScrollY){x=this.options.bounce?this.y+q/3:x>0?0:this.maxScrollY}if(!this.requestAnimationFrame){this._updateTranslate()}this.direction=r.deltaX>0?"right":"left";this.moved=true;this.x=w;this.y=x;k.trigger(this.scroller,"scroll",this)},_flick:function(o){if(!this.moved){return}o.stopPropagation();var n=o.detail;this._clearRequestAnimationFrame();if(o.type==="dragend"&&n.flick){return}var s=Math.round(this.x);var t=Math.round(this.y);this.isInTransition=false;if(this.resetPosition(this.options.bounceTime)){return}this.scrollTo(s,t);if(o.type==="dragend"){k.trigger(this.scroller,"scrollend",this);return}var u=0;var p="";if(this.options.momentum&&n.flickTime<300){var q=this.hasHorizontalScroll?this._momentum(this.x,n.flickDistanceX,n.flickTime,this.maxScrollX,this.options.bounce?this.wrapperWidth:0,this.options.deceleration):{destination:s,duration:0};var r=this.hasVerticalScroll?this._momentum(this.y,n.flickDistanceY,n.flickTime,this.maxScrollY,this.options.bounce?this.wrapperHeight:0,this.options.deceleration):{destination:t,duration:0};s=q.destination;t=r.destination;u=Math.max(q.duration,r.duration);this.isInTransition=true}if(s!=this.x||t!=this.y){if(s>0||s<this.maxScrollX||t>0||t<this.maxScrollY){p=j.quadratic}this.scrollTo(s,t,u,p);return}k.trigger(this.scroller,"scrollend",this)},_end:function(n){this.needReset=false;if((!this.moved&&this.needReset)||n.type===k.event.cancel){this.resetPosition()}},_transitionEnd:function(n){if(n.target!=this.scroller||!this.isInTransition){return}this._transitionTime();if(!this.resetPosition(this.options.bounceTime)){this.isInTransition=false;k.trigger(this.scroller,"scrollend",this)}},_scrollend:function(n){if((this.y===0&&this.maxScrollY===0)||(Math.abs(this.y)>0&&this.y<=this.maxScrollY)){k.trigger(this.scroller,"scrollbottom",this)}},_resize:function(){var n=this;clearTimeout(n.resizeTimeout);n.resizeTimeout=setTimeout(function(){n.refresh()},n.options.resizePolling)},_transitionTime:function(o){o=o||0;this.scrollerStyle.webkitTransitionDuration=o+"ms";if(this.indicators){for(var n=this.indicators.length;n--;){this.indicators[n].transitionTime(o)}}if(o){this.transitionTimer&&this.transitionTimer.cancel();this.transitionTimer=k.later(function(){k.trigger(this.scroller,"webkitTransitionEnd")},o+100,this)}},_transitionTimingFunction:function(n){this.scrollerStyle.webkitTransitionTimingFunction=n;if(this.indicators){for(var o=this.indicators.length;o--;){this.indicators[o].transitionTimingFunction(n)}}},_translate:function(n,o){this.x=n;this.y=o},_clearRequestAnimationFrame:function(){if(this.requestAnimationFrame){cancelAnimationFrame(this.requestAnimationFrame);this.requestAnimationFrame=null}},_updateTranslate:function(){var n=this;if(n.x!==n.lastX||n.y!==n.lastY){n.setTranslate(n.x,n.y)}n.requestAnimationFrame=requestAnimationFrame(function(){n._updateTranslate()})},_createScrollBar:function(n){var p=i.createElement("div");var o=i.createElement("div");p.className=f+" "+n;o.className=c;p.appendChild(o);if(n===h){this.scrollbarY=p;this.scrollbarIndicatorY=o}else{if(n===g){this.scrollbarX=p;this.scrollbarIndicatorX=o}}this.wrapper.appendChild(p);return p},_preventDefaultException:function(n,o){for(var p in o){if(o[p].test(n[p])){return true}}return false},_reLayout:function(){if(!this.hasHorizontalScroll){this.maxScrollX=0;this.scrollerWidth=this.wrapperWidth}if(!this.hasVerticalScroll){this.maxScrollY=0;this.scrollerHeight=this.wrapperHeight}this.indicators.map(function(r){r.refresh()});if(this.options.snap&&typeof this.options.snap==="string"){var p=this.scroller.querySelectorAll(this.options.snap);this.itemLength=0;this.snaps=[];for(var n=0,q=p.length;n<q;n++){var o=p[n];if(o.parentNode===this.scroller){this.itemLength++;this.snaps.push(o)}}this._initSnap()}},_momentum:function(n,q,u,s,v,o){var t=parseFloat(Math.abs(q)/u),p,r;o=o===undefined?0.0006:o;p=n+(t*t)/(2*o)*(q<0?-1:1);r=t/o;if(p<s){p=v?s-(v/2.5*(t/8)):s;q=Math.abs(p-n);r=q/t}else{if(p>0){p=v?v/2.5*(t/8):0;q=Math.abs(n)+p;r=q/t}}return{destination:Math.round(p),duration:r}},_getTranslateStr:function(n,o){if(this.options.hardwareAccelerated){return"translate3d("+n+"px,"+o+"px,0px) "+this.translateZ}return"translate("+n+"px,"+o+"px) "},setStopped:function(n){this.stopped=!!n},setTranslate:function(o,p){this.x=o;this.y=p;this.scrollerStyle.webkitTransform=this._getTranslateStr(o,p);if(this.indicators){for(var n=this.indicators.length;n--;){this.indicators[n].updatePosition()}}this.lastX=this.x;this.lastY=this.y;k.trigger(this.scroller,"scroll",this)},reLayout:function(){this.wrapper.offsetHeight;var q=parseFloat(k.getStyles(this.wrapper,"padding-left"))||0;var r=parseFloat(k.getStyles(this.wrapper,"padding-right"))||0;var s=parseFloat(k.getStyles(this.wrapper,"padding-top"))||0;var p=parseFloat(k.getStyles(this.wrapper,"padding-bottom"))||0;var o=this.wrapper.clientWidth;var n=this.wrapper.clientHeight;this.scrollerWidth=this.scroller.offsetWidth;this.scrollerHeight=this.scroller.offsetHeight;this.wrapperWidth=o-q-r;this.wrapperHeight=n-s-p;this.maxScrollX=Math.min(this.wrapperWidth-this.scrollerWidth,0);this.maxScrollY=Math.min(this.wrapperHeight-this.scrollerHeight,0);this.hasHorizontalScroll=this.options.scrollX&&this.maxScrollX<0;this.hasVerticalScroll=this.options.scrollY&&this.maxScrollY<0;this._reLayout()},resetPosition:function(n){var o=this.x,p=this.y;n=n||0;if(!this.hasHorizontalScroll||this.x>0){o=0}else{if(this.x<this.maxScrollX){o=this.maxScrollX}}if(!this.hasVerticalScroll||this.y>0){p=0}else{if(this.y<this.maxScrollY){p=this.maxScrollY}}if(o==this.x&&p==this.y){return false}this.scrollTo(o,p,n,this.options.scrollEasing);return true},_reInit:function(){var n=this.wrapper.querySelectorAll("."+d);for(var o=0,p=n.length;o<p;o++){if(n[o].parentNode===this.wrapper){this.scroller=n[o];break}}this.scrollerStyle=this.scroller&&this.scroller.style},refresh:function(){this._reInit();this.reLayout();k.trigger(this.scroller,"refresh",this);this.resetPosition()},scrollTo:function(p,q,o,n){var n=n||j.circular;this.isInTransition=o>0;if(this.isInTransition){this._clearRequestAnimationFrame();this._transitionTimingFunction(n.style);this._transitionTime(o);this.setTranslate(p,q)}else{this.setTranslate(p,q)}},scrollToBottom:function(o,n){o=o||this.options.scrollTime;this.scrollTo(0,this.maxScrollY,o,n)},gotoPage:function(n){this._gotoPage(n)},destroy:function(){this._initEvent(true);this.wrapper.setAttribute("data-scroll","")}});var l=function(o,n){this.wrapper=typeof n.el=="string"?i.querySelector(n.el):n.el;this.wrapperStyle=this.wrapper.style;this.indicator=this.wrapper.children[0];this.indicatorStyle=this.indicator.style;this.scroller=o;this.options=a.extend({listenX:true,listenY:true,fade:false,speedRatioX:0,speedRatioY:0},n);this.sizeRatioX=1;this.sizeRatioY=1;this.maxPosX=0;this.maxPosY=0;if(this.options.fade){this.wrapperStyle.webkitTransform=this.scroller.translateZ;this.wrapperStyle.webkitTransitionDuration="0ms";this.wrapperStyle.opacity="0"}};l.prototype={handleEvent:function(n){},transitionTime:function(n){n=n||0;this.indicatorStyle.webkitTransitionDuration=n+"ms"},transitionTimingFunction:function(n){this.indicatorStyle.webkitTransitionTimingFunction=n},refresh:function(){this.transitionTime();if(this.options.listenX&&!this.options.listenY){this.indicatorStyle.display=this.scroller.hasHorizontalScroll?"block":"none"}else{if(this.options.listenY&&!this.options.listenX){this.indicatorStyle.display=this.scroller.hasVerticalScroll?"block":"none"}else{this.indicatorStyle.display=this.scroller.hasHorizontalScroll||this.scroller.hasVerticalScroll?"block":"none"}}this.wrapper.offsetHeight;if(this.options.listenX){this.wrapperWidth=this.wrapper.clientWidth;this.indicatorWidth=Math.max(Math.round(this.wrapperWidth*this.wrapperWidth/(this.scroller.scrollerWidth||this.wrapperWidth||1)),8);this.indicatorStyle.width=this.indicatorWidth+"px";this.maxPosX=this.wrapperWidth-this.indicatorWidth;this.minBoundaryX=0;this.maxBoundaryX=this.maxPosX;this.sizeRatioX=this.options.speedRatioX||(this.scroller.maxScrollX&&(this.maxPosX/this.scroller.maxScrollX))}if(this.options.listenY){this.wrapperHeight=this.wrapper.clientHeight;this.indicatorHeight=Math.max(Math.round(this.wrapperHeight*this.wrapperHeight/(this.scroller.scrollerHeight||this.wrapperHeight||1)),8);this.indicatorStyle.height=this.indicatorHeight+"px";this.maxPosY=this.wrapperHeight-this.indicatorHeight;this.minBoundaryY=0;this.maxBoundaryY=this.maxPosY;this.sizeRatioY=this.options.speedRatioY||(this.scroller.maxScrollY&&(this.maxPosY/this.scroller.maxScrollY))}this.updatePosition()},updatePosition:function(){var n=this.options.listenX&&Math.round(this.sizeRatioX*this.scroller.x)||0,o=this.options.listenY&&Math.round(this.sizeRatioY*this.scroller.y)||0;if(n<this.minBoundaryX){this.width=Math.max(this.indicatorWidth+n,8);this.indicatorStyle.width=this.width+"px";n=this.minBoundaryX}else{if(n>this.maxBoundaryX){this.width=Math.max(this.indicatorWidth-(n-this.maxPosX),8);this.indicatorStyle.width=this.width+"px";n=this.maxPosX+this.indicatorWidth-this.width}else{if(this.width!=this.indicatorWidth){this.width=this.indicatorWidth;this.indicatorStyle.width=this.width+"px"}}}if(o<this.minBoundaryY){this.height=Math.max(this.indicatorHeight+o*3,8);this.indicatorStyle.height=this.height+"px";o=this.minBoundaryY}else{if(o>this.maxBoundaryY){this.height=Math.max(this.indicatorHeight-(o-this.maxPosY)*3,8);this.indicatorStyle.height=this.height+"px";o=this.maxPosY+this.indicatorHeight-this.height}else{if(this.height!=this.indicatorHeight){this.height=this.indicatorHeight;this.indicatorStyle.height=this.height+"px"}}}this.x=n;this.y=o;this.indicatorStyle.webkitTransform=this.scroller._getTranslateStr(n,o)},fade:function(q,o){if(o&&!this.visible){return}clearTimeout(this.fadeTimeout);this.fadeTimeout=null;var p=q?250:500,n=q?0:300;q=q?"1":"0";this.wrapperStyle.webkitTransitionDuration=p+"ms";this.fadeTimeout=setTimeout((function(r){this.wrapperStyle.opacity=r;this.visible=+r}).bind(this,q),n)}};k.Scroll=m;a.fn.scroll=function(n){return new m(a(this),n)}})(window.jQuery,window.fui,document);(function(a,t,s){var p="f-pull-top-pocket";var j="f-pull-bottom-pocket";var i="f-pull";var o="f-pull-loading";var k="f-pull-caption";var l="f-pull-caption-down";var n="f-pull-caption-refresh";var m="f-pull-caption-nomore";var d="f-icon";var q="f-spinner";var e="f-icon-pulldown";var b="f-block";var c="f-hidden";var r="f-visibility";var h=o+" "+d+" "+e;var g=o+" "+d+" "+e;var f=o+" "+d+" "+q;var u=['<div class="'+i+'">','<div class="{icon}"></div>','<div class="'+k+'">{contentrefresh}</div>',"</div>"].join("");var v={init:function(w,x){this._super(w,a.extend(true,{scrollY:true,scrollX:false,indicators:true,deceleration:0.003,down:{height:50,contentinit:"下拉可以刷新",contentdown:"下拉可以刷新",contentover:"释放立即刷新",contentrefresh:"正在刷新..."},up:{height:50,auto:false,contentinit:"上拉显示更多",contentdown:"上拉显示更多",contentrefresh:"正在加载...",contentnomore:"没有更多数据了",duration:300}},x))},_init:function(){this._super();this._initPocket()},_initPulldownRefresh:function(){this.pulldown=true;if(this.topPocket){this.pullPocket=this.topPocket;this.pullPocket.classList.add(b);this.pullPocket.classList.add(r);this.pullCaption=this.topCaption;this.pullLoading=this.topLoading}},_initPullupRefresh:function(){this.pulldown=false;if(this.bottomPocket){this.pullPocket=this.bottomPocket;this.pullPocket.classList.add(b);this.pullPocket.classList.add(r);this.pullCaption=this.bottomCaption;this.pullLoading=this.bottomLoading}},_initPocket:function(){var w=this.options;if(w.down&&w.down.hasOwnProperty("callback")){this.topPocket=this.scroller.querySelector("."+p);if(!this.topPocket){this.topPocket=this._createPocket(p,w.down,g);this.wrapper.insertBefore(this.topPocket,this.wrapper.firstChild)}this.topLoading=this.topPocket.querySelector("."+o);this.topCaption=this.topPocket.querySelector("."+k)}if(w.up&&w.up.hasOwnProperty("callback")){this.bottomPocket=this.scroller.querySelector("."+j);if(!this.bottomPocket){this.bottomPocket=this._createPocket(j,w.up,f);this.scroller.appendChild(this.bottomPocket)}this.bottomLoading=this.bottomPocket.querySelector("."+o);this.bottomCaption=this.bottomPocket.querySelector("."+k);this.wrapper.addEventListener("scrollbottom",this)}},_createPocket:function(w,y,x){var z=s.createElement("div");z.className=w;z.innerHTML=u.replace("{contentrefresh}",y.contentinit).replace("{icon}",x);return z},_resetPullDownLoading:function(){var w=this.pullLoading;if(w){this.pullCaption.innerHTML=this.options.down.contentdown;w.style.webkitTransition="";w.style.webkitTransform="";w.style.webkitAnimation="";w.className=g}},_setCaptionClass:function(x,w,y){if(!x){switch(y){case this.options.up.contentdown:w.className=k+" "+l;break;case this.options.up.contentrefresh:w.className=k+" "+n;break;case this.options.up.contentnomore:w.className=k+" "+m;break}}},_setCaption:function(D,B){if(this.loading){return}var z=this.options;var A=this.pullPocket;var w=this.pullCaption;var y=this.pullLoading;var x=this.pulldown;var C=this;if(A){if(B){setTimeout(function(){w.innerHTML=C.lastTitle=D;if(x){y.className=g}else{C._setCaptionClass(false,w,D);y.className=f}y.style.webkitAnimation="";y.style.webkitTransition="";y.style.webkitTransform=""},100)}else{if(D!==this.lastTitle){w.innerHTML=D;if(x){if(D===z.down.contentrefresh){y.className=f;y.style.webkitAnimation="spinner-spin 1s step-end infinite"}else{if(D===z.down.contentover){y.className=h;y.style.webkitTransition="-webkit-transform 0.3s ease-in";y.style.webkitTransform="rotate(180deg)"}else{if(D===z.down.contentdown){y.className=g;y.style.webkitTransition="-webkit-transform 0.3s ease-in";y.style.webkitTransform="rotate(0deg)"}}}}else{if(D===z.up.contentrefresh){y.className=f+" "+r}else{y.className=f+" "+c}C._setCaptionClass(false,w,D)}this.lastTitle=D}}}}};t.PullRefresh=v})(window.jQuery,window.fui,document);(function(a,e,h,d,g){var c="f-visibility";var b="f-hidden";var f=e.Scroll.extend(a.extend({handleEvent:function(i){this._super(i);if(i.type==="scrollbottom"){if(i.target===this.scroller){this._scrollbottom()}}},_scrollbottom:function(){if(!this.pulldown&&!this.loading){this.pulldown=false;this._initPullupRefresh();this.pullupLoading()}},_start:function(i){if(i.touches&&i.touches.length&&i.touches[0].clientX>30){i.target&&!this._preventDefaultException(i.target,this.options.preventDefaultException)&&i.preventDefault()}if(!this.loading){this.pulldown=this.pullPocket=this.pullCaption=this.pullLoading=false}this._super(i)},_drag:function(i){this._super(i);if(!this.pulldown&&!this.loading&&this.topPocket&&i.detail.direction==="down"&&this.y>=0){this._initPulldownRefresh()}if(this.pulldown){this._setCaption(this.y>this.options.down.height?this.options.down.contentover:this.options.down.contentdown)}},_reLayout:function(){this.hasVerticalScroll=true;this._super()},resetPosition:function(i){if(this.pulldown){if(this.y>=this.options.down.height){this.pulldownLoading(g,i||0);return true}else{!this.loading&&this.topPocket.classList.remove(c)}}return this._super(i)},pulldownLoading:function(k,j){typeof k==="undefined"&&(k=this.options.down.height);this.scrollTo(0,k,j,this.options.bounceEasing);if(this.loading){return}this._initPulldownRefresh();this._setCaption(this.options.down.contentrefresh);this.loading=true;this.indicators.map(function(l){l.fade(0)});var i=this.options.down.callback;i&&i.call(this)},endPulldownToRefresh:function(){var i=this;if(i.topPocket&&i.loading&&this.pulldown){i.scrollTo(0,0,i.options.bounceTime,i.options.bounceEasing);i.loading=false;i._setCaption(i.options.down.contentdown,true);setTimeout(function(){i.loading||i.topPocket.classList.remove(c)},350)}},pullupLoading:function(i,k,j){k=k||0;this.scrollTo(k,this.maxScrollY,j,this.options.bounceEasing);if(this.loading){return}this._initPullupRefresh();this._setCaption(this.options.up.contentrefresh);this.indicators.map(function(l){l.fade(0)});this.loading=true;i=i||this.options.up.callback;i&&i.call(this)},endPullupToRefresh:function(i){var j=this;if(j.bottomPocket){j.loading=false;if(i){this.finished=true;j._setCaption(j.options.up.contentnomore);j.wrapper.removeEventListener("scrollbottom",j)}else{j._setCaption(j.options.up.contentdown);j.loading||j.bottomPocket.classList.remove(c)}}},disablePullupToRefresh:function(){this._initPullupRefresh();this.bottomPocket.className="f-pull-bottom-pocket "+b;this.wrapper.removeEventListener("scrollbottom",this)},enablePullupToRefresh:function(){var i=this.pulldown;this._initPullupRefresh();this.bottomPocket.classList.remove(b);this._setCaption(this.options.up.contentdown);if(i){this.pulldown=true;this.bottomPocket.classList.remove(c)}this.wrapper.addEventListener("scrollbottom",this)},refresh:function(i){if(i&&this.finished){this.enablePullupToRefresh();this.finished=false}this._super()},},e.PullRefresh));a.fn.pullRefresh=function(i){return new f(a(this),i)}})(window.jQuery,window.fui,window,document);(function(a,k,q,j,p){var e="f-slider";var f="f-slider-group";var i="f-slider-loop";var g="f-slider-indicator";var c="f-action-previous";var b="f-action-next";var h="f-slider-item";var d="f-active";var m="."+h;var l="."+g;var n=".f-slider-progress-bar";var o=k.Scroll.extend({init:function(r,v){v=v||{};r.addClass(e);var s=a('<div class="f-slider-group" ></div>');r.append(s);var w=this;if(v.loop==p){w.loop=true}if(!!v.indicator){w.$indicator=a('<div class="'+g+'" ></div>');r.append(w.$indicator)}var t=v.data||[];var u=t.length;if(u==0){return}else{if(u==1){s.append('<div class="'+h+'"><div><img src="'+t[0]+'"></div></div>')}else{if(w.loop){s.append('<div class="'+h+' f-slider-item-duplicate"><div><img src="'+t[u-1]+'"></div></div>')}a.each(t,function(x,y){if(!!v.indicator){w.$indicator.append('<div class="f-indicator '+(x==0?d:"")+'" ></div>')}s.append('<div class="'+h+" "+(x==0?d:"")+' "><div><img src="'+y+'"></div></div>')});if(w.loop){s.append('<div class="'+h+' f-slider-item-duplicate"><div><img src="'+t[0]+'"></div></div>')}}}this._super(s,a.extend(true,{fingers:1,interval:0,scrollY:false,scrollX:true,indicators:false,scrollTime:1000,startX:false,slideTime:0,snap:m},v));this.options.gotonum!=p&&this.options.gotonum>0&&this.gotoItem(this.options.gotonum,0)},_init:function(){this._reInit();if(this.scroller){this.scrollerStyle=this.scroller.style;this.progressBar=this.wrapper.querySelector(n);if(this.progressBar){this.progressBarWidth=this.progressBar.offsetWidth;this.progressBarStyle=this.progressBar.style}this._super();this._initTimer()}},_triggerSlide:function(){var s=this;s.isInTransition=false;var r=s.currentPage;s.slideNumber=s._fixedSlideNumber();if(s.loop&&s.itemLength>1){if(s.slideNumber===0){s.setTranslate(s.pages[1][0].x,0)}else{if(s.slideNumber===s.itemLength-3){s.setTranslate(s.pages[s.itemLength-2][0].x,0)}}}if(s.lastSlideNumber!=s.slideNumber){s.lastSlideNumber=s.slideNumber;s.lastPage=s.currentPage;k.trigger(s.wrapper,"slide",{slideNumber:s.slideNumber})}s._initTimer()},_handleSlide:function(u){var C=this;if(u.target!==C.wrapper){return}var t=u.detail;t.slideNumber=t.slideNumber||0;var D=C.scroller.querySelectorAll(m);var z=[];for(var v=0,A=D.length;v<A;v++){var y=D[v];if(y.parentNode===C.scroller){z.push(y)}}var r=t.slideNumber;if(C.loop){r+=1}if(!C.wrapper.classList.contains("f-segmented-control")){for(var v=0,A=z.length;v<A;v++){var y=z[v];if(y.parentNode===C.scroller){if(v===r){y.classList.add(d)}else{y.classList.remove(d)}}}}var x=C.wrapper.parentNode.querySelector(".f-slider-indicator");if(x){if(x.getAttribute("data-scroll")){a(x).scroll().gotoPage(t.slideNumber)}var w=x.querySelectorAll(".f-indicator");if(w.length>0){for(var v=0,A=w.length;v<A;v++){w[v].classList[v===t.slideNumber?"add":"remove"](d)}}else{var B=x.querySelector(".f-number span");if(B){B.innerText=(t.slideNumber+1)}else{var s=x.querySelectorAll(".f-control-item");for(var v=0,A=s.length;v<A;v++){s[v].classList[v===t.slideNumber?"add":"remove"](d)}}}}u.stopPropagation()},_handleTabShow:function(r){var s=this;s.gotoItem((r.detail.tabNumber||0),s.options.slideTime)},_handleIndicatorTap:function(r){var s=this;var t=r.target;if(t.classList.contains(c)||t.classList.contains(b)){s[t.classList.contains(c)?"prevItem":"nextItem"]();r.stopPropagation()}},_initEvent:function(s){var t=this;t._super(s);var r=s?"removeEventListener":"addEventListener";t.wrapper[r]("slide",this);t.wrapper[r]("show.f.tab",this)},handleEvent:function(r){this._super(r);switch(r.type){case"slide":this._handleSlide(r);break;case"show.f.tab":if(~this.snaps.indexOf(r.target)){this._handleTabShow(r)}break}},_scrollend:function(r){this._super(r);this._triggerSlide(r)},_drag:function(s){this._super(s);var r=s.detail.direction;if(r==="left"||r==="right"){var t=this.wrapper.getAttribute("data-slidershowTimer");t&&q.clearTimeout(t);s.stopPropagation()}},_initTimer:function(){var s=this;var t=s.wrapper;var r=s.options.interval;var u=t.getAttribute("data-slidershowTimer");u&&q.clearTimeout(u);if(r){u=q.setTimeout(function(){if(!t){return}if(!!(t.offsetWidth||t.offsetHeight)){s.nextItem(true)}s._initTimer()},r);t.setAttribute("data-slidershowTimer",u)}},_fixedSlideNumber:function(r){r=r||this.currentPage;var s=r.pageX;if(this.loop){if(r.pageX===0){s=this.itemLength-3}else{if(r.pageX===(this.itemLength-1)){s=0}else{s=r.pageX-1}}}return s},_reLayout:function(){this.hasHorizontalScroll=true;this._super()},_getScroll:function(){var r=a.parseTranslateMatrix(a.getStyles(this.scroller,"webkitTransform"));return r?r.x:0},_transitionEnd:function(r){if(r.target!==this.scroller||!this.isInTransition){return}this._transitionTime();this.isInTransition=false;k.trigger(this.wrapper,"scrollend",this)},_flick:function(t){if(!this.moved){return}var r=t.detail;var s=r.direction;this._clearRequestAnimationFrame();this.isInTransition=true;if(t.type==="flick"){if(r.deltaTime<200){this.x=this._getPage((this.slideNumber+(s==="right"?-1:1)),true).x}this.resetPosition(this.options.bounceTime)}else{if(t.type==="dragend"&&!r.flick){this.resetPosition(this.options.bounceTime)}}t.stopPropagation()},_initSnap:function(){this.scrollerWidth=this.itemLength*this.scrollerWidth;this.maxScrollX=Math.min(this.wrapperWidth-this.scrollerWidth,0);this._super();if(!this.currentPage.x){var r=this.pages[this.loop?1:0];r=r||this.pages[0];if(!r){return}this.currentPage=r[0];this.slideNumber=0;this.lastSlideNumber=typeof this.lastSlideNumber==="undefined"?0:this.lastSlideNumber}else{this.slideNumber=this._fixedSlideNumber();this.lastSlideNumber=typeof this.lastSlideNumber==="undefined"?this.slideNumber:this.lastSlideNumber}this.options.startX=this.currentPage.x||0},_getSnapX:function(r){return Math.max(-r,this.maxScrollX)},_getPage:function(s,r){if(this.loop){if(s>(this.itemLength-(r?2:3))){s=1;time=0}else{if(s<(r?-1:0)){s=this.itemLength-2;time=0}else{s+=1}}}else{if(!r){if(s>(this.itemLength-1)){s=0;time=0}else{if(s<0){s=this.itemLength-1;time=0}}}s=Math.min(Math.max(0,s),this.itemLength-1)}if(s<0){s=0}if(this.itemLength==1){return this.pages[0][0]}return this.pages[s][0]},_gotoItem:function(r,s){this.currentPage=this._getPage(r,true);this.scrollTo(this.currentPage.x,0,s,this.options.scrollEasing);if(s===0){k.trigger(this.wrapper,"scrollend",this)}},setTranslate:function(s,t){this._super(s,t);var r=this.progressBar;if(r){this.progressBarStyle.webkitTransform=this._getTranslateStr((-s*(this.progressBarWidth/this.wrapperWidth)),0)}},resetPosition:function(r){r=r||0;if(this.x>0){this.x=0}else{if(this.x<this.maxScrollX){this.x=this.maxScrollX}}this.currentPage=this._nearestSnap(this.x);this.scrollTo(this.currentPage.x,0,r,this.options.scrollEasing);return true},gotoItem:function(r,s){this._gotoItem(r,typeof s==="undefined"?this.options.scrollTime:s)},nextItem:function(){this._gotoItem(this.slideNumber+1,this.options.scrollTime)},prevItem:function(){this._gotoItem(this.slideNumber-1,this.options.scrollTime)},getSlideNumber:function(){return this.slideNumber||0},_reInit:function(){var r=this.wrapper.querySelectorAll("."+f);for(var s=0,t=r.length;s<t;s++){if(r[s].parentNode===this.wrapper){this.scroller=r[s];break}}this.scrollerStyle=this.scroller&&this.scroller.style;if(this.progressBar){this.progressBarWidth=this.progressBar.offsetWidth;this.progressBarStyle=this.progressBar.style}},refresh:function(r){if(r){a.extend(this.options,r);this._super();this._initTimer()}else{this._super()}},destroy:function(){this._initEvent(true);this.wrapper.setAttribute("data-slider","")}});a.fn.slider=function(r){return new o(a(this),r)}})(window.jQuery,window.fui,window,document);(function(a,h,d){var b="f-zoom";var c="f-zoom-scroller";var i="."+b;var j="."+c;var g="pinchstart";var e="pinch";var f="pinchend";if("ongesturestart" in window){g="gesturestart";e="gesturechange";f="gestureend"}a.Zoom=function(l,C){var L=this;L.options=a.extend(a.Zoom.defaults,C);L.wrapper=L.element=l;L.scroller=l.querySelector(j);L.scrollerStyle=L.scroller&&L.scroller.style;L.zoomer=l.querySelector(i);L.zoomerStyle=L.zoomer&&L.zoomer.style;L.init=function(){h.options.gestureConfig.pinch=true;h.options.gestureConfig.doubletap=true;L.initEvents()};L.initEvents=function(N){var M=N?"removeEventListener":"addEventListener";var O=L.scroller;O[M](g,L.onPinchstart);O[M](e,L.onPinch);O[M](f,L.onPinchend);O[M](h.event.start,L.onTouchstart);O[M](h.event.move,L.onTouchMove);O[M](h.event.cancel,L.onTouchEnd);O[M](h.event.end,L.onTouchEnd);O[M]("drag",L.dragEvent);O[M]("doubletap",L.doubleTapEvent)};L.dragEvent=function(M){if(p||A){M.stopPropagation()}};L.doubleTapEvent=function(M){L.toggleZoom(M.detail.center)};L.transition=function(M,N){N=N||0;M.webkitTransitionDuration=N+"ms";return L};L.translate=function(M,N,O){N=N||0;O=O||0;M.webkitTransform="translate3d("+N+"px,"+O+"px,0px)";return L};L.scale=function(N,M){M=M||1;N.webkitTransform="translate3d(0,0,0) scale("+M+")";return L};L.scrollerTransition=function(M){return L.transition(L.scrollerStyle,M)};L.scrollerTransform=function(M,N){return L.translate(L.scrollerStyle,M,N)};L.zoomerTransition=function(M){return L.transition(L.zoomerStyle,M)};L.zoomerTransform=function(M){return L.scale(L.zoomerStyle,M)};var D=1,k=1,B=false,A=false;L.onPinchstart=function(M){A=true};L.onPinch=function(M){if(!B){L.zoomerTransition(0);B=true}D=(M.detail?M.detail.scale:M.scale)*k;if(D>L.options.maxZoom){D=L.options.maxZoom-1+Math.pow((D-L.options.maxZoom+1),0.5)}if(D<L.options.minZoom){D=L.options.minZoom+1-Math.pow((L.options.minZoom-D+1),0.5)}L.zoomerTransform(D)};L.onPinchend=function(M){D=Math.max(Math.min(D,L.options.maxZoom),L.options.minZoom);L.zoomerTransition(L.options.speed).zoomerTransform(D);k=D;B=false};L.setZoom=function(M){D=k=M;L.scrollerTransition(L.options.speed).scrollerTransform(0,0);L.zoomerTransition(L.options.speed).zoomerTransform(D)};L.toggleZoom=function(Q,R){if(typeof Q==="number"){R=Q;Q=undefined}R=typeof R==="undefined"?L.options.speed:R;if(D&&D!==1){D=k=1;L.scrollerTransition(R).scrollerTransform(0,0)}else{D=k=L.options.maxZoom;if(Q){var N=h.offset(L.zoomer);var S=N.top;var M=N.left;var O=(Q.x-M)*D;var P=(Q.y-S)*D;this._cal();if(O>=r&&O<=(r+K)){O=r-O+K/2}else{if(O<r){O=r-O+K/2}else{if(O>(r+K)){O=r+K-O-K/2}}}if(P>=s&&P<=(s+J)){P=s-P+J/2}else{if(P<s){P=s-P+J/2}else{if(P>(s+J)){P=s+J-P-J/2}}}O=Math.min(Math.max(O,t),r);P=Math.min(Math.max(P,u),s);L.scrollerTransition(R).scrollerTransform(O,P)}else{L.scrollerTransition(R).scrollerTransform(0,0)}}L.zoomerTransition(R).zoomerTransform(D)};L._cal=function(){K=L.wrapper.offsetWidth;J=L.wrapper.offsetHeight;z=L.zoomer.offsetWidth;o=L.zoomer.offsetHeight;var N=z*D;var M=o*D;t=Math.min((K/2-N/2),0);r=-t;u=Math.min((J/2-M/2),0);s=-u};var K,J,q,p,m,n,t,u,r,s,z,o,y={},x={},v,w,E,G,H,F,I;L.onTouchstart=function(M){M.preventDefault();q=true;y.x=M.type==="touchstart"?M.targetTouches[0].pageX:M.pageX;y.y=M.type==="touchstart"?M.targetTouches[0].pageY:M.pageY};L.onTouchMove=function(M){M.preventDefault();if(!q){return}if(!p){K=L.wrapper.offsetWidth;J=L.wrapper.offsetHeight;z=L.zoomer.offsetWidth;o=L.zoomer.offsetHeight;var P=h.parseTranslateMatrix(h.getStyles(L.scroller,"webkitTransform"));v=P.x||0;w=P.y||0;L.scrollerTransition(0)}var O=z*D;var N=o*D;if(O<K&&N<J){return}t=Math.min((K/2-O/2),0);r=-t;u=Math.min((J/2-N/2),0);s=-u;x.x=M.type===h.event.move?M.targetTouches[0].pageX:M.pageX;x.y=M.type===h.event.move?M.targetTouches[0].pageY:M.pageY;if(!p&&!B){if((Math.floor(t)===Math.floor(v)&&x.x<y.x)||(Math.floor(r)===Math.floor(v)&&x.x>y.x)){q=false;return}}p=true;m=x.x-y.x+v;n=x.y-y.y+w;if(m<t){m=t+1-Math.pow((t-m+1),0.8)}if(m>r){m=r-1+Math.pow((m-r+1),0.8)}if(n<u){n=u+1-Math.pow((u-n+1),0.8)}if(n>s){n=s-1+Math.pow((n-s+1),0.8)}if(!E){E=x.x}if(!F){F=x.y}if(!G){G=h.now()}H=(x.x-E)/(h.now()-G)/2;I=(x.y-F)/(h.now()-G)/2;if(Math.abs(x.x-E)<2){H=0}if(Math.abs(x.y-F)<2){I=0}E=x.x;F=x.y;G=h.now();L.scrollerTransform(m,n)};L.onTouchEnd=function(M){if(!M.touches||!M.touches.length){A=false}if(!q||!p){q=false;p=false;return}q=false;p=false;var Q=300;var R=300;var N=H*Q;var S=m+N;var O=I*R;var T=n+O;if(H!==0){Q=Math.abs((S-m)/H)}if(I!==0){R=Math.abs((T-n)/I)}var P=Math.max(Q,R);m=S;n=T;var V=z*D;var U=o*D;t=Math.min((K/2-V/2),0);r=-t;u=Math.min((J/2-U/2),0);s=-u;m=Math.max(Math.min(m,r),t);n=Math.max(Math.min(n,s),u);L.scrollerTransition(P).scrollerTransform(m,n)};L.destroy=function(){L.initEvents(true);delete h.data[L.wrapper.getAttribute("data-zoomer")];L.wrapper.setAttribute("data-zoomer","")};L.init();return L};a.Zoom.defaults={speed:300,maxZoom:3,minZoom:1,};a.fn.zoom=function(k){var l=[];this.each(function(){var o=null;var n=this;var m=n.getAttribute("data-zoomer");if(!m){m=h.guid();h.data[m]=o=new a.Zoom(n,k);n.setAttribute("data-zoomer",m)}else{o=h.data[m]}l.push(o)});return l.length===1?l[0]:l}})(window.jQuery,window.fui,document);(function(a,d,c){var e=null;var b={init:function(g){var f=a("#fui_imagePreview");if(f.length==0){f=a('<div id="fui_imagePreview"  class="f-imagePreview" ><div class="f-imagePreview-slider" ></div></div>');a("body").append(f);b.bindEvent(f)}f.addClass("f-imagePreview-in");f[0].dfop=g;f.show();setTimeout(function(){f.removeClass("f-imagePreview-in");f.find(".f-imagePreview-slider").slider({data:g.data,loop:true,indicator:true,gotonum:g.gotonum});var h=f.find("img");h.addClass("f-zoom");h.parent().addClass("f-zoom-scroller");h.parent().parent().addClass("f-zoom-wrapper").zoom();f.find("img").each(function(){var i=a(this)[0];i.addEventListener("doubletap",function(){e&&e.cancel();e=null})});f=null},500)},bindEvent:function(f){f.on("tap",function(h){var i=h.target||h.srcElement;var g=a(this);var j=g[0].dfop;if(j.taphold){g=null;j.taphold=false;return false}if(i.tagName!=="IMG"){b.close(g)}else{b.laterCloseEvent()}g=null});f.on("taphold",function(){var g=a(this);var i=g[0].dfop;var h="";if(g.find("img").length===1){h=g.find("img").attr("src")}else{h=g.find(".f-slider-item.f-active img").attr("src")}i.holdEvent&&i.holdEvent(h);i.taphold=true;g=null;return false})},close:function(f){e=null;f.addClass("f-imagePreview-out");setTimeout(function(){f.hide();f.removeClass("f-imagePreview-out");var g=f.find("img");var h=g.parent().parent().addClass("f-zoom-wrapper").zoom();a.each(h,function(i,j){j&&j.destroy&&j.destroy()});f.find(".f-imagePreview-slider").remove();f.append('<div class="f-imagePreview-slider" ></div>');g=null;f=null},500)},laterCloseEvent:function(){!e&&(e=d.later(function(){b.close(a("#fui_imagePreview"))},300))}};d.imagePreview=function(f){if(f==undefined||!f.data||f.data.length==0){return}b.init(f)};a.fn.imagePreviewClose=function(){b.close(a(this))}})(window.jQuery,window.fui,document);(function(a){a.fn.toptab=function(f,e){var b=a(this);f=f||[];if(f.length>0){b.addClass("f-toptab");var c=a('<div class="f-toptab-btns" ><div></div></div>');var d=a('<div class="f-toptab-content" ></div>');a.each(f,function(i,j){var g='<a class="f-toptab-btn'+(i==0?" f-active ":"")+'"  data-value="'+i+'"  href="#">'+j+"</a>";c.find("div").append(g);var h='<div class="f-toptab-content-item'+(i==0?" f-active ":"")+'" data-value="'+i+'" ></div>';d.append(h)});b.append(c);b.append(d);c[0].callback=e;c.find(".f-toptab-btn").on("tap",function(){var h=a(this);var i=h.parent().parent();var g=i.next();if(!h.hasClass("f-active")){i.find(".f-active").removeClass("f-active");h.addClass("f-active");g.find(".f-active").removeClass("f-active");var k=h.attr("data-value");g.find('[data-value="'+k+'"]').addClass("f-active");var j=i[0].callback;j&&j(k)}h=null;i=null;g=null});return d.find(".f-toptab-content-item")}return null}})(window.jQuery);(function(a){a.fn.ftimeline=function(f){var c=a(this);c.addClass("f-timeline");var e=a('<div class="f-timeline-allwrap"></div>');var d=a("<ul></ul>");var b=a('<li class="f-timeline-header"><div>当前</div></li>');d.append(b);a.each(f,function(j,k){var i=a('<li class="f-timeline-item" ><div class="f-timeline-wrap" ></div></li>');if(j==0){i.find("div").addClass("f-timeline-current")}var h=i.find(".f-timeline-wrap");var g=a('<div class="f-timeline-content"><span class="arrow"></span></div>');g.append('<div class="f-timeline-title">'+k.title+"</div>");g.append('<div class="f-timeline-body"><span>'+k.people+"</span>"+k.content+"</div>");h.append('<span class="f-timeline-date">'+k.time+"</span>");h.append(g);d.append(i)});d.append('<li class="f-timeline-ender"><div>开始</div></li>');e.html(d);c.html(e)}})(window.jQuery);(function(a,c){var b={init:function(d,g){var f='            <div class="f-popright" >                <div class="f-popright-content">                <div class="f-popright-title" ><div class="f-popright-btn-left">';if(g.cancelBtn){f+='<div class="f-popright-btn cancel" data-value="cancel" >'+g.cancelBtn+"</div>"}if(g.restBtn){f+='<div class="f-popright-btn rest" data-value="rest">'+g.restBtn+"</div>"}f+="</div>";if(g.okBtn){f+='<div class="f-popright-btn ok" data-value="ok">'+g.okBtn+"</div>"}f+='</div>                <div class="f-popright-body" ></div>            </div>        </div>';var e=a(f);d.parent().append(e);e.find(".f-popright-body").append(d);d.addClass("f-popright-bodyContent");e.find(".f-popright-body").scroll();b.bindEvent(e,g);d=null;e=null},bindEvent:function(d,e){d.on("tap",function(g){g=g||window.event;var h=g.target||g.srcElement;var f=a(h);if(f.hasClass("f-popright")){f.removeClass("active")}});d.find(".f-popright-btn").on("tap",{fop:e},function(h){var f=a(this);var i=f.attr("data-value");var g=h.data.fop;g.callBack&&g.callBack(i,f.parents(".f-popright").find(".f-popright-bodyContent"));if(i=="ok"||i=="cancel"){f.parents(".f-popright").removeClass("active")}});d=null}};a.fn.fpopright=function(f){var e={okBtn:"确定",restBtn:"重置",cancelBtn:"取消",callBack:false};var d=a(this);if(d[0].fop){return d}a.extend(e,f||{});d[0].fop=e;b.init(d,e);return d};a.fn.fpoprightShow=function(){var e=a(this);var d=e.parents(".f-popright");if(!d.hasClass("active")){d.addClass("active")}d=null;e=null};a.fn.fpoprightHide=function(){var e=a(this);var d=e.parents(".f-popright");if(d.hasClass("active")){d.removeClass("active")}d=null;e=null}})(window.jQuery,window.fui);(function(a){var v=false;var s=false;var c=false;var u=false;var w=false;var x=0;var l=0;var t=0;var j=false;var k=false;var n=false;var f="f-swipebtn-handler";var g="f-swipebtn-right";var d="f-swipebtn-btn";var h="f-swipebtn-transitioning";var e="f-swipebtn-selected";var p="."+f;var q="."+g;var o="."+d;var m=0.8;var y=function(){var z;if(x!==l){if(c&&c.length>0){n=x/t;if(x<-t){x=-t-Math.pow(-x-t,m)}for(var B=0,C=c.length;B<C;B++){var A=c[B];if(typeof A._buttonOffset==="undefined"){A._buttonOffset=A.offsetLeft}z=A._buttonOffset;r(A,(x-z*(1+Math.max(n,-1))))}}r(v,x);l=x}w=requestAnimationFrame(function(){y()})};var r=function(z,A){if(z&&z.style){z.style.webkitTransform="translate("+A+"px,0)"}};var i={handleEvent:function(z){switch(z.type){case"drag":this.drag(z);break;case"dragend":this.dragend(z);break;case"swiperight":this.swiperight(z);break;case"swipeleft":this.swipeleft(z);break}},drag:function(F){F=F||window.event;var E=F.target||F.srcElement;var B=null;var z=a(E);if(z.hasClass("f-swipebtn")){B=z[0]}else{B=z.parents(".f-swipebtn")[0]}z=null;if(!j){v=s=c=u=w=false;v=B.querySelector(p);if(v){s=B.querySelector(q);if(s){t=s.offsetWidth;c=s.querySelectorAll(o)}B.classList.remove(h);k=B.classList.contains(e)}}var C=F.detail;var D=C.direction;var A=C.angle;if(D==="left"&&(A>150||A<-150)){if(c){j=true}}else{if(D==="right"&&(A>-30&&A<30)){if((c&&k)){j=true}}}if(j){F.stopPropagation();F.detail.gesture.preventDefault();var G=F.detail.deltaX;if(k){G=G-t}if((G<0&&!c)){if(!k){return}G=0}if(G<0){u="toLeft"}else{if(G>0){u="toRight"}else{if(!u){u="toLeft"}}}if(!w){y()}x=G}},swipeleft:function(z){if(j){z.stopPropagation()}},swiperight:function(z){if(j){z.stopPropagation()}},dragend:function(J){if(!j){return}J=J||window.event;var I=J.target||J.srcElement;var G=null;var z=a(I);if(z.hasClass("f-swipebtn")){G=z[0]}else{G=z.parents(".f-swipebtn")[0]}z=null;J.stopPropagation();if(w){cancelAnimationFrame(w);w=null}var H=J.detail;j=false;var A="close";var B=u==="toLeft"?t:0;var L=H.swipe||(Math.abs(x)>B/2);if(L){if(!k){A="open"}else{if(H.direction==="left"){A="open"}}}G.classList.add(h);var F;if(A==="open"){var N=u==="toLeft"?-B:B;r(v,N);F=u==="toLeft"?c:"undefined";if(typeof F!=="undefined"){var C=null;for(var K=0;K<F.length;K++){C=F[K];r(C,N)}C.parentNode.classList.add(e);G.classList.add(e);if(!k){fui.trigger(G,u==="toLeft"?"slideleft":"slideright")}}}else{r(v,0);s&&s.classList.remove(e);G.classList.remove(e)}var D;if(c&&c.length>0&&c!==F){for(var K=0,M=c.length;K<M;K++){var E=c[K];D=E._buttonOffset;if(typeof D==="undefined"){E._buttonOffset=E.offsetLeft}r(E,-D)}}}};var b=function(z){var A=z[0];A.addEventListener("drag",i);A.addEventListener("dragend",i);A.addEventListener("swiperight",i);A.addEventListener("swipeleft",i)};a.fn.fswipebtn=function(){a(this).each(function(){b(a(this))})}})(window.jQuery);(function(a,c){var b={init:function(d,f){f.id=c.guid();var e='<div class="f-checkbox f-pop" id="pop_'+f.id+'"  data-page="'+c.pageid()+'"  >';e+='<div class="f-checkbox-header" >';e+='<div class="f-checkbox-cancel">取消</div>';e+='<div class="f-checkbox-ok">确定</div>';e+="</div>";e+='<div class="f-checkbox-body" >';f.dataMap={};a.each(f.data,function(g,h){f.dataMap[h[f.ivalue]]=h;e+='<div class="f-checkbox-item" data-value="'+h[f.ivalue]+'" >'+h[f.itext]+"</div>"});e+="</div></div>";a("body").append(e);c.createMask();b.bindEvent(d,f)},bindEvent:function(e,f){e.on("tap",function(){var h=a(this);if(h.attr("readonly")||h.parents(".lr-form-row").attr("readonly")){return false}var j=h[0];var i=j.fop;i.callback&&i.callback();var g=a("#pop_"+i.id);if(!g.hasClass("active")){g.addClass("active");c.showMask()}g.find(".selected").removeClass("selected");setTimeout(function(){if(i.value!=undefined&&i.value!=""&&i.value!=null){var k=i.value.split(",");a.each(k,function(l,m){g.find('[data-value="'+m+'"]').addClass("selected")})}g=null},300);return false});var d=a("#pop_"+f.id);d.find(".f-checkbox-body").scroll();d.find(".f-checkbox-body").on("tap",function(h){h=h||window.event;var i=h.target||h.srcElement;var g=a(i);if(g.hasClass("f-checkbox-item")){if(g.hasClass("selected")){g.removeClass("selected")}else{g.addClass("selected")}return false}});d.find(".f-checkbox-cancel").on("tap",function(){var g=a(this).parents(".f-checkbox");g.removeClass("active");c.hideMask();return false});d.find(".f-checkbox-ok").on("tap",{$self:e,fop:f},function(j){var g=a(this).parents(".f-checkbox");g.removeClass("active");c.hideMask();j=j||window.event;var k=j.data.fop;var h=j.data.$self;var l=[];var m=[];var i=[];g.find(".selected").each(function(){var o=a(this).attr("data-value");var n=k.dataMap[o];m.push(o);l.push(n[k.itext]);i.push(n)});if(k.value!=String(m)){k.value=String(m);k.text=String(l);h.trigger("change");if(!!k.change){k.change(k.value,k.text,i,h)}}g=null;h=null;return false});d=null}};a.fn.fcheckbox=function(f){var d=a(this);if(d.length===0){return d}if(d[0].fop){return d}var e={data:[],change:false,ivalue:"value",itext:"text"};a.extend(e,f||{});d[0].fop=e;b.init(d,e);return d};a.fn.fcheckboxSet=function(i){var d=a(this);if(d.length>0){var g=d[0].fop;if(i!=undefined&&i!=""&&i!=null){var j=i.split(",");var h=[];var f=[];var e=[];a.each(j,function(k,m){if(m){e.push(m);var l=g.dataMap[m];if(l){h.push(l[g.itext]||"");f.push(l)}}});g.value=String(e);g.text=String(h);d.trigger("change");if(!!g.change){g.change(g.value,g.text,f,d)}}}d=null};a.fn.fcheckboxSetData=function(f){var e=a(this);if(e.length>0){var g=e[0].fop;if(g){g.dataMap={};g.data=f||[];var d=a("#pop_"+g.id+" .f-scroll");d.html("");a.each(g.data,function(h,i){g.dataMap[i[g.ivalue]]=i;d.append('<div class="f-checkbox-item" data-value="'+i[g.ivalue]+'" >'+i[g.itext]+"</div>")});d=null}}e=null}})(window.jQuery,window.fui);/*
 * 版 本 Learun-Mobile V2.0.0 力软敏捷开发框架(http://www.learun.cn)
 * Copyright (c) 2013-2018 上海力软信息技术有限公司
 * 创建人：力软-前端开发组
 * 日 期：2017.12.15
 * 描 述：力软移动端框架 基础方法
 */
(function ($, fui, window) {
    "use strict";
    
    //cordova.js 方法

    $.fn.getHexBackgroundColor = function () {
        var rgb = $(this).css('background-color');
        rgb = rgb.match(/^rgb\((\d+),\s*(\d+),\s*(\d+)\)$/);
        function hex(x) { return ("0" + parseInt(x).toString(16)).slice(-2); }
        return rgb = "#" + hex(rgb[1]) + hex(rgb[2]) + hex(rgb[3]);
    }

    var backbuttonEvent = function () {
        // 判断是否有图片预览
        if ($('#fui_imagePreview').length >0 && !$('#fui_imagePreview').is(':hidden')) {
            $('#fui_imagePreview').imagePreviewClose();
        }
        else if ($('.f-dialog').length > 0) {
            $('.f-dialog').remove();
            $('.f-dialog-mask').remove();
        }
        else {
            fui.loading(false);
            // 判断下当前页面是否有返回键
            var pageInfo = fui.nav.page.data[fui.nav.page.activeid];
            if (pageInfo.op.isBack) {
                window.lrmui.nav.closeCurrent();
            }
            else {
                window.lrmui.layer.confirm('是否退出APP', function (_index) {
                    if (_index === '1') {
                        navigator.app.exitApp();
                    }
                }, '力软提示', ['否', '是']);
            }
        }
    }

    window.lrmui = {// 力软mobileui框架基础方法
        isreal: false,
        isready: false,
        init: function (callback) {
            document.addEventListener("backbutton", backbuttonEvent, false);
            _init(callback);
        },
        guid: function (separator) {
            return fui.guid(separator);
        },
        pageid: function () {
            return fui.pageid();
        },
        deviceId: function () {// 设备id
            var deviceId = window.lrmui.storage.get('deviceId');
            if (!deviceId) {
                deviceId = window.device ? device.uuid : fui.guid('');
                window.lrmui.storage.set('deviceId', deviceId);
            }
            return deviceId;
        },
        type: function (obj) {
            return fui.type(obj);
        },
        date: {// 日期格式化操作
            parse: function (v) {
                return fui.date.parse(v);
            },
            format: function (v, format) {
                return fui.date.format(v, format);
            }
        },
        actionsheet: function (op) {
            /*id: '',
             data: [], //text:'名称'，group:'组名'，event:'点击事件'，mark:'' 标记后为红色
             cancel: false*/
            fui.actionsheet(op);
        },
        layer: {
            toast: function (msg) {// 消失提示框
                fui.dialog({ msg: msg });
            },
            warning: function (msg, callback, title, btn) {// 警告框
                fui.dialog({ type: 'warning', msg: msg, title: title, callback: callback, btn: btn });
            },
            confirm: function (msg, callback, title, btns) {// 确认消息框
                fui.dialog({ type: 'confirm', msg: msg, title: title, callback: callback, btns: btns });
            },
            popinput: function (msg, callback, title, btns, input) {// 输入消息框
                fui.dialog({ type: 'prompt', msg: msg, title: title, callback: callback, btns: btns });
            },
            loading: function (isShow, msg) {// 加载提示框
                fui.loading(isShow, msg);
            }
        },
        md5: function (str) {
            return $.md5(str);
        }
    };
    function _init(callback) {// 框架初始化方法
        if (window.lrmui.isready) {
            // 开始执行初始化函数
            // 处理 Cordova 暂停并恢复事件
            //document.addEventListener('pause', onPause.bind(this), false);
            //document.addEventListener('resume', onResume.bind(this), false);
            callback();
        }
        else {
            setTimeout(function () {
                _init(callback);
            }, 200);
        }
    }
    function isPlatform() {// 调试浏览器模拟环境，浏览器环境，安卓，IOS
        if (!window.cordova) {  // 首先判断是不是浏览器环境
            window.lrmui.platform = 'browser';
            window.lrmui.isready = true; // 表示设备加载完成
        }
        else {// 只有在真机还有模拟环境下才启用
            document.addEventListener('deviceready', onDeviceReady.bind(this), false);// 注册设备是否初始化完成并绑定相应的方法
        }
    }
    isPlatform();
    function onDeviceReady() {
        // 判断当前平台
        if (window.navigator.platform === 'Win32') {  // 判断是不是调试浏览器模拟环境
            window.lrmui.platform = 'simulator';
        }
        else {// 真机环境
            window.lrmui.isreal = true;
            window.lrmui.platform = cordova.platformId;
            StatusBar.styleDefault();
        }
        window.lrmui.isready = true; // 表示设备加载完成
    }

    $(function () {
        $('body').delegate('input[type="password"],input[type="text"],textarea,.lrtextarea', 'tap', function () {
            var $this = $(this);
            if ($this.attr('readonly') || $this.parents('.lr-form-row').attr('readonly')) {
                return false;
            }

            if (!$this.is(":focus")) {
                if ($this.hasClass('lrtextarea')) {
                    var range;
                    if (window.getSelection) {//ie11 10 9 ff safari
                        range = window.getSelection();//创建range
                        range.selectAllChildren($this[0]);//range 选择obj下所有子内容
                        $this.focus(); //解决ff不获取焦点无法定位问题
                        range.collapseToEnd();//光标移至最后
                    }
                    else if (document.selection) {//ie10 9 8 7 6 5
                        range = document.selection.createRange();//创建选择对象
                        range.moveToElementText($this[0]);//range定位到obj
                        range.collapse(false);//光标移至最后
                        range.select();
                    }
                }
                else
                {
                    $this.focus();
                }
            }
        });
    });
})(window.jQuery, window.fui, window);/*
 * 版 本 Learun-Mobile V2.0.0 力软敏捷开发框架(http://www.learun.cn)
 * Copyright (c) 2013-2018 上海力软信息技术有限公司
 * 创建人：力软-前端开发组
 * 日 期：2017.12.15
 * 描 述：力软移动端框架 ajax请求方法
 */
(function ($, learun, fui, window) {
    "use strict";

    learun.http = {
        post: function (url, param, callback, dataType) {
            return learun.http.async('POST', url, param, callback, dataType);
        },
        get: function (url, param, callback, dataType) {
            return learun.http.async('GET', url, param, callback, dataType);
        },
        webget: function (url, param, callback, dataType) {

            return $.ajax({
                url: url,
                data: param,
                type: 'GET',
                dataType: dataType || "json",
                async: true,
                cache: false,
                success: function (res) {
                    callback(res);
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    callback(null);
                },
                beforeSend: function () {
                },
                complete: function () {
                }
            });
        },
        async: function (type, url, param, callback, dataType) {
            if (window.lrmui.isreal) {
                if (type === 'GET') {
                    window.cordovaHTTP.get(url, param, {}, function (res) {
                        callback(JSON.parse(res.data));
                    }, function () {
                        callback(null);
                    });
                }
                else {
                    window.cordovaHTTP.post(url, param, {}, function (res) {
                        callback(JSON.parse(res.data));
                    }, function () {
                        callback(null);
                    });
                }
            }
            else {
                return $.ajax({
                    url: url,
                    data: param,
                    type: type,
                    dataType: dataType || "json",
                    async: true,
                    cache: false,
                    success: function (res) {
                        callback(res);
                    },
                    error: function (XMLHttpRequest, textStatus, errorThrown) {
                        callback(null);
                    },
                    beforeSend: function () {
                    },
                    complete: function () {
                    }
                });
            }
        }
    };

})(window.jQuery, window.lrmui, window.fui, window);/*
 * 版 本 Learun-Mobile V2.0.0 力软敏捷开发框架(http://www.learun.cn)
 * Copyright (c) 2013-2018 上海力软信息技术有限公司
 * 创建人：力软-前端开发组
 * 日 期：2017.12.15
 * 描 述：力软移动端框架 导航路由方法（框架核心部分）
 */
(function ($, learun, fui, window) {
    "use strict";

    learun.nav = {
        data: {// 加载过的页面数据|开发的时候请改完代码保存后刷新下页面
        },
        getpage: function (name) {
            var page = this.data[name];
            var pageobj = null;
            if (page) {
                pageobj = page.jsObj;
            }
            return pageobj;
        },
        go: function (op) {
            var dfop = {
                path: '',
                title: '',
                type: '',// 'right','bottom', 不填为淡入淡出
                backbtn: '<i class="iconfont icon-back_light"></i>',
                isBack: true,
                isTab: false,
                isHead: true,
                fclass: '',
                param: {}// 传递参数
            };
            $.extend(dfop, op || {}); 
            if (dfop.path === '') {
                fui.dialog({ msg: '力软信息提示：不知道页面路径！' });
                return false;
            }

            dfop.start = function (pageinfo) {
                // 页面开始
                if (!pageinfo.op.isTab) {
                    $('.lr-tabbar').css('z-index', 2);
                }
            };

            dfop.end = function (pageinfo) {
                var $tab = $('.lr-tabbar');
                $tab.css('z-index', 10);
                // 页面动画结束
                if (pageinfo.op.isTab) {
                    $tab.show();
                }
                else {
                    $tab.hide();
                }
                var $pagebody = pageinfo.$page.find('.f-page-body');
                var page = learun.nav.data[pageinfo.op.id];

                if (!pageinfo.op.isBack || !pageinfo.op.isHead) {
                    // 如果跳转的页面没有返回的按钮，就把之前带返回按钮的页面删掉
                    var pageid = "";
                    var _pageinfo = pageinfo;
                    for (var i = 0; i < 100; i++) {
                        pageid = _pageinfo.preid;
                        if (pageid !== "" && pageinfo.id !== pageid) {
                            _pageinfo = fui.nav.page.data[pageid];
                            if (!_pageinfo.op.isBack || !_pageinfo.op.isHead) {
                                break;
                            }
                            else {
                                fui.nav.close(pageid);
                            }
                        }
                        else {
                            break;
                        }
                    }
                    pageinfo.preid = "";
                }

                if (!pageinfo.hasLoad) {
                    loadhtml(page, $pagebody, pageinfo.op);
                }
                else {// 如果页面已经加载过了,如果页面有reload方法就加载一次
                    page.jsObj && page.jsObj.reload && page.jsObj.reload($pagebody, pageinfo);
                    // 检测当前头部颜色，并进行设置
                    if (window.lrmui.isreal) {
                        if (page.jsObj && page.jsObj.headColor) {
                            StatusBar.backgroundColorByHexString(page.jsObj.headColor);
                        }
                        else {
                            var _$header = $pagebody.parent().find('.f-page-header');
                            if (_$header.length !== 0) {
                                var _color = _$header.getHexBackgroundColor();
                                StatusBar.backgroundColorByHexString(_color);
                            }
                            _$header = null;
                        }
                    }
                }
            };

            dfop.bfdestroy = function (pageinfo) {
                // 页面销毁之前执行
                var page = learun.nav.data[pageinfo.op.id];
                if (!!page.jsObj && !!page.jsObj.beforedestroy) {
                    return page.jsObj.beforedestroy();
                }
                return true;
            };
            dfop.destroy = function (pageinfo) {
                // 页面销毁
                var page = learun.nav.data[pageinfo.op.id];
                if (!!page.jsObj && !!page.jsObj.destroy) {
                    page.jsObj.destroy();
                }
            };

            // 去加载页面数据
            load(dfop.path);

            // 渲染页面
            if (!dfop.isBack) {
                dfop.fclass = "lr-page-no-back";
            }

            if (!dfop.isHead) {
                dfop.fclass = dfop.fclass + " lr-page-no-head";
            }



            if (dfop.isTab) {
                dfop.fclass = dfop.fclass + " lr-page-have-tab";
            }
            dfop.id = dfop.path;
            fui.nav.go(dfop);

        },
        closeCurrent: function () {
            fui.nav.closeCurrent();
        },
        close: function (id) {
            fui.nav.close(id);
        }
    };
    function load(path) {
        if (!learun.nav.data[path]) {// 判断下当前页面是否已经加载了
            learun.nav.data[path] = {
                //cssLoaded: false,
                jsLoaded: false,
                htmlLoaded: false,
                strhtml: ''
            };
            var paths = path.split('/');
            var filename = paths[paths.length - 1];
            // 加载页面css代码
            learun.http.webget('pages/' + path + '/' + filename + '.css', {}, function (res) {
                if (res !== null) {
                    fui.includeCss(res);
                }
            }, 'text');
            //fui.loadCss('pages/' + path + '/' + path + '.css');
            // 加载页面html代码
            learun.http.webget('pages/' + path + '/' + filename + '.html', {}, function (res) {
                if (res === null) {
                    fui.dialog({ msg: '力软信息提示：找不到页面！' });
                }
                learun.nav.data[path].strhtml = res || '';
                learun.nav.data[path].htmlLoaded = true;
            }, 'text');
            // 加载页面js代码
            learun.http.webget('pages/' + path + '/' + filename + '.js', {}, function (res) {
                var strjs = '(function ($,learun) {learun.nav.data["' + path + '"].jsObj = ' + (res || 'null;') + '})(window.jQuery,window.lrmui);';
                fui.includeJs(strjs);
                learun.nav.data[path].jsLoaded = true;
            }, 'text');
        }
    }


    // 加载js代码
    function loadjs(_page, _$pagebody, _op) {
        if (_page.jsLoaded) {
            if (_page.jsObj) {
                if (_page.jsObj.isScroll) {
                    _$pagebody.scroll();
                }
                setTimeout(function () {
                    _page.jsObj.init && _page.jsObj.init(_$pagebody, _op.param);
                    // 检测当前头部颜色，并进行设置
                    if (window.lrmui.isreal) {
                        if (_page.jsObj.headColor) {
                            StatusBar.backgroundColorByHexString(_page.jsObj.headColor);
                        }
                        else {
                            var _$header = _$pagebody.parent().find('.f-page-header');
                            if (_$header.length !== 0) {
                                var _color = _$header.getHexBackgroundColor();
                                StatusBar.backgroundColorByHexString(_color);
                            }
                            _$header = null;
                        }

                        
                    }
                }, 100);
            }
        }
        else {
            setTimeout(function () {
                loadjs(_page, _$pagebody);
            }, 200);
        }
    }
    // 加载html代码
    function loadhtml(_page, _$pagebody, _op) {
        if (_page.htmlLoaded) {
            _$pagebody.html(_page.strhtml);
            loadjs(_page, _$pagebody, _op);
        }
        else {
            setTimeout(function () {
                loadhtml(_page, _$pagebody, _op);
            }, 200);
        }
    }
})(window.jQuery, window.lrmui, window.fui, window);/*
 * 版 本 Learun-Mobile V2.0.0 力软敏捷开发框架(http://www.learun.cn)
 * Copyright (c) 2013-2018 上海力软信息技术有限公司
 * 创建人：力软-前端开发组
 * 日 期：2017.12.15
 * 描 述：力软移动端框架 底部选项卡导航（框架核心部分）
 * 修改日志：2018-02-02 修正go方法无法跳转页面的问题
 */
(function ($, learun, fui, window) {
    "use strict";

    learun.tab = {
        init: function (tabdata) {
            // 首先加载tab数据
            var $tab = $('.lr-tabbar');
            var _html = '';
            $.each(tabdata, function (_index, item) {
                if (item.icon) {
                    _html += '\
                    <a class="lr-tab-button" data-value="'+ item.page + '" href="#">\
                        <i class="unselected iconfont '+ item.icon + '"></i>\
                        <i class="selected iconfont '+ item.fillicon + '"></i>\
                        <span class="lr-tab-button-text">'+ item.text + '</span>\
                    </a>';
                }
                else {
                    _html += '\
                    <a class="lr-tab-button" data-value="'+ item.page + '" href="#">\
                        <img class="unselected" src="'+ item.img + '">\
                        <img class="selected" src="'+ item.fillimg +'">\
                        <span class="lr-tab-button-text">'+ item.text + '</span>\
                    </a>';
                }
               
            });
            $tab.html(_html);
            $tab.on("tap", ".lr-tab-button", function (e) {
                var $this = $(this);
                if ($this.hasClass('active')) {
                    return false;
                }
                var page = $this.attr('data-value');
                var title = $this.find('span').text();

                learun.nav.go({ path: page, title: title, isBack: false, isTab: true });

                $this.parent().find('.active').removeClass('active');
                $this.addClass('active');
            });
        },
        go: function (page) {
            $('.lr-tabbar [data-value="' + page + '"]').removeClass('active');
            $('.lr-tabbar [data-value="' + page + '"]').trigger('tap');
        }
    };

})(window.jQuery, window.lrmui, window.fui, window);/*
 * 版 本 Learun-Mobile V2.0.0 力软敏捷开发框架(http://www.learun.cn)
 * Copyright (c) 2013-2018 上海力软信息技术有限公司
 * 创建人：力软-前端开发组
 * 日 期：2017.12.15
 * 描 述：力软移动端框架 客户端存储（框架核心部分）
 */
(function ($, learun, fui, window) {
    "use strict";
    learun.storage = {
        get: function (name) {// 获取
            return JSON.parse(localStorage.getItem(name));
        },
        set: function (name, value) {// 设置
            localStorage.setItem(name, JSON.stringify(value));
        }
    };
})(window.jQuery, window.lrmui, window.fui, window);
/*
 * 版 本 Learun-Mobile V2.0.0 力软敏捷开发框架(http://www.learun.cn)
 * Copyright (c) 2013-2018 上海力软信息技术有限公司
 * 创建人：力软-前端开发组
 * 日 期：2017.12.15
 * 描 述：力软移动端框架 日期选择插件
 */
(function ($, learun, fui, window) {
    "use strict";

    // 日期选择插件
    $.fn.lrdate = function (op) {
        var $this = $(this);
        var dfop = {
            placeholder: '请选择',
            type: 'datetime',//datetime:2017-10-12 00:00;date:2017-10-12; time:00:00; month:2017-10
            label: ['年', '月', '日', '时', '分'],
            change: false    // 选择时间改变的时候触发

        };
        $.extend(dfop, op || {});
        dfop.callback = function () {
            learun.formblur();
        };

        $this.attr('type', 'lrdate');
        $this.addClass('lr-date');
        $this.html('<div class="text">' + dfop.placeholder + '</div>');
        setTimeout(function () {
            $this.fdtpicker(dfop).on('change', function () {
                var $self = $(this);
                $self.find('.text').text($self[0].fop.value);
            });
        }, 100);
        return $this;
    };
    // 日期选择插件（设置数据）
    $.fn.lrdateSet = function (value) {
        var $this = $(this);

        function set($this, value) {
            if ($this[0].fop) {
                if (value) {
                    switch ($this[0].fop.type) {
                        case 'datetime':
                            value = fui.date.format(value, 'yyyy-MM-dd hh:mm');
                            break;
                        case 'date':
                            value = fui.date.format(value, 'yyyy-MM-dd');
                            break;
                        case 'time':
                            _value = fui.date.format(value, 'hh:mm');
                            if (!_value) {
                                _value = fui.date.format('2017-12-18 ' + value, 'hh:mm');
                            }
                            value = _value;
                            break;
                        case 'month':
                            value = fui.date.format(value, 'yyyy-MM');
                            break;
                    }
                    $this[0].fop.value = value;
                    $this.find('.text').text(value);
                }
                else {
                    $this[0].fop.value = '';
                    $this.find('.text').text($this[0].fop.placeholder);
                }
            }
            else {
                setTimeout(function () {
                    set($this, value);
                }, 100);
            }
        }
        set($this, value);
    };
    // 日期选择插件（获取数据）
    $.fn.lrdateGet = function () {
        var $this = $(this);
        return $this[0].fop.value;
    };
})(window.jQuery, window.lrmui, window.fui, window);


/*
 * 版 本 Learun-Mobile V2.0.0 力软敏捷开发框架(http://www.learun.cn)
 * Copyright (c) 2013-2018 上海力软信息技术有限公司
 * 创建人：力软-前端开发组
 * 日 期：2017.12.15
 * 描 述：力软移动端框架 选择器插件
 */
(function ($, learun, fui, window) {
    "use strict";

    // 选择器(初始化)
    $.fn.lrpicker = function (op) {
        var $this = $(this);
        if ($this.length === 0) {
            return $this;
        }
        var dfop = {
            placeholder: '请选择',
            data: [],
            level: 1,
            ivalue: 'value',
            itext: 'text'
        };
        $.extend(dfop, op || {});
        dfop.callback = function () {
            learun.formblur();
        };
        $this.attr('type', 'lrpicker');
        $this.addClass('lr-picker');
        $this.html('<div class="text">' + dfop.placeholder + '</div>');

        setTimeout(function () {
            $this.fpoppicker(dfop).on('change', function () {
                var $self = $(this);
                var text = $self[0].fop.text;
                $self.find('.text').text(text.replace(/,/g, '/'));
            });
        }, 100);

        return $this;
    };
    // 选择器(获取数据值)
    $.fn.lrpickerGet = function (type) {
        var $this = $(this);
        if ($this.length === 0) {
            return "";
        }
        var fop = $this[0].fop;
        if (type === 'text') {
            return fop.text;
        }
        else {
            return fop.value;
        }
    };
    // 选择器(设置数据值)
    $.fn.lrpickerSet = function (value) {
        var $this = $(this);
        if ($this.length === 0) {
            return false;
        }
        function set(value, $this) {
            if (!$this[0].fop) {
                setTimeout(function () {
                    set(value, $this);
                }, 100);
            }
            else {
                var fop = $this[0].fop;
                if (value) {
                    fop._lrTmpValue = value;
                }
                $this.fpoppickerSet(value);
                if (value === '') {
                    fop.value = '';
                    fop.text = '';
                    $this.find('.text').text(fop.placeholder);
                }
                else if (fop.text !== "" && fop.text !== undefined && fop.text !== null) {
                    $this.find('.text').text(fop.text.replace(/,/g, '/'));
                }
            }
        }
        set(value, $this);
    };      
    // 选择器(更新数据)
    $.fn.lrpickerSetData = function (data) {
        var $this = $(this);
        if ($this.length === 0) {
            return false;
        }
        function updateData(data, $this) {
            if (!$this[0].fop) {
                setTimeout(function () {
                    updateData(data, $this);
                }, 100);
            }
            else {
                $this.fpoppickerSetData(data);
                $this.lrpickerSet($this[0].fop._lrTmpValue);
            }
        }
        updateData(data, $this);
    };

})(window.jQuery, window.lrmui, window.fui, window);

/*
 * 版 本 Learun-Mobile V2.0.0 力软敏捷开发框架(http://www.learun.cn)
 * Copyright (c) 2013-2018 上海力软信息技术有限公司
 * 创建人：力软-前端开发组
 * 日 期：2017.12.15
 * 描 述：力软移动端框架 开关插件
 */
(function ($, learun, fui, window) {
    "use strict";
    // 开关插件(初始化)
    $.fn.lrswitch = function () {
        var $this = $(this);
        $this.attr('type', 'lrswitch');
        $this.on('tap', function () {
            learun.formblur();
        });
        return $this.fswitch();
    };
    // 开关插件(设置值)1选中 0 没有选中
    $.fn.lrswitchSet = function (value) {
        $(this).fswitchSet(value);
    };
    // 开关插件(获取值)
    $.fn.lrswitchGet = function () {
        return $(this).fswitchGet();
    };
})(window.jQuery, window.lrmui, window.fui, window);/*
 * 版 本 Learun-Mobile V2.0.0 力软敏捷开发框架(http://www.learun.cn)
 * Copyright (c) 2013-2018 上海力软信息技术有限公司
 * 创建人：力软-前端开发组
 * 日 期：2017.12.15
 * 描 述：力软移动端框架 表单方法插件
 */
(function ($, learun, fui, window) {
    "use strict";
    // 表单验证
    $.fn.lrformValid = function () {
        var validateflag = true;
        var validHelper = fui.validator;
        $(this).find("[isvalid=yes]").each(function () {

            var $this = $(this);
            var checkexpession = $(this).attr("checkexpession");
            var checkfn = validHelper['is' + checkexpession];
            if (!checkexpession || !checkfn) { return false; }
            var errormsg = $(this).attr("errormsg") || "";
            // 获取数据值
            var value;
            if ($this.hasClass('lrtextarea')) {
                value = $this.text();
            }
            else {
                var type = $this.attr('type');
                if (type === 'lrpicker') {
                    value = $this.lrpickerGet();
                }
                else if (type === 'lrdate') {
                    value = $this.lrdateGet();
                }
                else if (type === 'lrcheckbox') {
                    value = $this.lrcheckboxGet();
                }
                else if (type === 'lrselect') {
                    value = $this.lrselectGet();
                }
                else {
                    value = $this.val();
                }
            }


            var r = { code: true, msg: '' };
            if (checkexpession === 'LenNum' || checkexpession === 'LenNumOrNull' || checkexpession === 'LenStr' || checkexpession === 'LenStrOrNull') {
                var len = $this.attr("length");
                r = checkfn(value, len);
            } else {
                r = checkfn(value);
            }
            if (!r.code) {
                validateflag = false;
                fui.dialog({ msg: errormsg + r.msg });
                return false;
            }
        });
        return validateflag;
    };
    // 获取表单数据
    $.fn.lrformGet = function (keyValue) {
        var resdata = {};
        $(this).find('input,textarea,.lr-picker,.lr-date,.f-switch,.lrtextarea,.lr-checkbox,.lr-select,.lr-imagepicker').each(function (r) {
            var $this = $(this);
            var id = $this.attr('id');
            if (id) {
                var type = $this.attr('type');
                switch (type) {
                    case "lrpicker":
                        resdata[id] = $this.lrpickerGet() || '';
                        break;
                    case "lrdate":
                        resdata[id] = $this.lrdateGet() || '';
                        break;
                    case "lrswitch":
                        resdata[id] = $this.lrswitchGet();
                        break;
                    case "lrcheckbox":
                        resdata[id] = $this.lrcheckboxGet();
                        break;
                    case "lrselect":
                        resdata[id] = $this.lrselectGet();
                        break;
                    case "lrimagepicker":
                        resdata[id] = $this.imagepickerGet();
                        break;
                    default:
                        if ($this.hasClass('lrtextarea')) {
                            var value1 = $this.text() || '';
                            resdata[id] = $.trim(value1);
                        }
                        else {
                            var value2 = $this.val() || '';
                            resdata[id] = $.trim(value2);
                        }
                        break;
                }
                resdata[id] += '';
                if (resdata[id] === '') {
                    resdata[id] = '&nbsp;';
                }
                if (resdata[id] === '&nbsp;' && !keyValue) {
                    resdata[id] = '';
                }
            }
        });
        return resdata;
    };
    // 设置表单数据
    $.fn.lrformSet = function (data) {
        var $this = $(this);
        for (var id in data) {
            var value = data[id];
            var $obj = $this.find('#' + id);
            if ($obj.length >= 0 && value !== null) {
                var type = $obj.attr('type');
                switch (type) {
                    case "lrpicker":
                        $obj.lrpickerSet(value);
                        break;
                    case "lrdate":
                        $obj.lrdateSet(value);
                        break;
                    case "lrswitch":
                        $obj.lrswitchSet(value);
                        break;
                    case "lrcheckbox":
                        $obj.lrcheckboxSet(value);
                        break;
                    case "lrselect":
                        $obj.lrselectSet(value);
                        break;
                    case "lrimagepicker":
                        resdata[id] = $this.imagepickerSet(value);
                        break;
                    default:
                        if ($obj.hasClass('lrtextarea')) {
                            $obj.text(value);
                        }
                        else {
                            $obj.val(value);
                        }
                        break;
                }
            }
        }
    };

    learun.formblur = function () {// 是输入框失去焦点,隐藏输入键盘
        // 失去焦点
        var pageid = learun.pageid();
        $('#' + pageid).find('input[type="password"]:focus,input[type="text"]:focus,textarea:focus,.lrtextarea:focus').blur();
        if (learun.isreal) {// 真机调试下隐藏键盘
            window.Keyboard.hide();
        }
    };

})(window.jQuery, window.lrmui, window.fui, window);


/*
 * 版 本 Learun-Mobile V2.0.0 力软敏捷开发框架(http://www.learun.cn)
 * Copyright (c) 2013-2018 上海力软信息技术有限公司
 * 创建人：力软-前端开发组
 * 日 期：2017.12.15
 * 描 述：力软移动端框架 分页列表插件
 */
(function ($, learun, fui, window) {
    "use strict";

    // 列表分页加载
    var _lrpagination = {
        renderData: function (op, data, page, records) {
            op.lrpage = page;
            op.lrrecords = records;
            op.lrtotal = parseInt(parseInt(records) / parseInt(op.lrrows)) + (parseInt(records) % parseInt(op.lrrows) > 0 ? 1 : 0);
            if (data) {
                var startnum = (page - 1) * op.lrrows + 1;
                for (var i = 0, l = data.length; i < l; i++) {
                    op.lrdata.push(data[i]);
                    var _html = '';
                    var $item = null;
                  
                    if (op.lrbtns.length > 0) {
                        $item = $('\
                        <div data-index="' + (startnum + i - 1) + '" class="lr-page-item f-swipebtn" >\
                            <div class="f-swipebtn-handler"></div>\
                            <div class="f-swipebtn-right"></div>\
                        </div>');
                        _html = op.lrrenderData(startnum + i, data[i], $item.find('.f-swipebtn-handler'));
                    }
                    else {
                        $item = $('<div data-index="' + (startnum + i - 1) + '" class="lr-page-item" ></div>');
                        _html = op.lrrenderData(startnum + i, data[i], $item);
                    }

                    if (_html !== '') {
                        var _$html = $(_html);
                        _$html.attr('data-index',startnum + i - 1);
                        _$html.addClass('lr-page-item');
                        op.$list.append(_$html);

                        if (op.lrbtns.length > 0) {
                            _$html.addClass('f-swipebtn');
                            var $handler = $('<div class="f-swipebtn-handler">' + _$html.html() + '</div>');
                            var $btn = $('<div class="f-swipebtn-right"></div>');
                            _$html.html($handler);
                            _$html.append($btn);
                            $.each(op.lrbtns, function (_index, _item) {
                                var _$item = $(_item).addClass('f-swipebtn-btn');
                                $btn.append(_$item);
                            });
                            _$html.fswipebtn();

                            $handler = null;
                            $btn = null;
                        }
                        _$html = null;
                    } else {
                        op.$list.append($item);
                        if (op.lrbtns.length > 0) {
                            var $btns = $item.find('.f-swipebtn-right');
                            $.each(op.lrbtns, function (_index, _item) {
                                var _$item = $(_item).addClass('f-swipebtn-btn');
                                $btns.append(_$item);
                            });
                            $btns = null;
                            $item.fswipebtn();
                        }
                    }
                    $item = null;
                }
            }
        },
        reload: function () {
            fui.loading(true, '加载数据中');
            var self = this;
            var op = self.options;
            var pageparam = {
                page: 1,
                rows: op.lrrows
            };
            op.lrgetData && op.lrgetData(pageparam, function (data, records) {
                op.$list.html("");
                op.lrdata = [];
                _lrpagination.renderData(op, data, 1, records);
                fui.loading(false);
                op = null;
                self = null;
            });
        }
    };
    $.fn.lrpagination = function (op) {
        var dfop = {
            lclass: 'lr-list',
            rows: 10,                  // 每页行数
            getData: function (param, callback) {// 获取数据 param 分页参数,callback 异步回调
                callback([], 0);
            },
            renderData: function (_index, _item) {// 渲染数据模板
                return '';
            },
            click: false, // item, $self 点击事件,
            down: {
                contentinit: '下拉可以刷新',
                contentdown: '下拉可以刷新',
                contentover: '释放立即刷新',
                contentrefresh: '正在刷新...'
            },
            up: {
                contentinit: '上拉显示更多',
                contentdown: '上拉显示更多',
                contentrefresh: '正在加载...',
                contentnomore: '没有更多数据了'
            }
        };
        $.extend(dfop, op || {});
        var $this = $(this);
        $this[0].lrop = dfop;

        var fop = {
            lrdata: [],
            lrpage: 1,                   // 当前页
            lrrecords: 0,                // 总记录数
            lrtotal: 0,                  // 总页数

            lrrows: dfop.rows,
            lrgetData: dfop.getData,
            lrrenderData: dfop.renderData,
            lrbtns: dfop.btns || [],
            lrclick: dfop.click,

            down: {
                contentinit: dfop.down.contentinit,
                contentdown: dfop.down.contentdown,
                contentover: dfop.down.contentover,
                contentrefresh: dfop.down.contentrefresh,
                callback: function () {
                    var self = this;
                    var op = self.options;
                    var pageparam = {
                        page: 1,
                        rows: op.lrrows
                    };

                    op.lrgetData && op.lrgetData(pageparam, function (data, records) {
                        op.$list.html("");
                        op.lrdata = [];
                        _lrpagination.renderData(op, data, 1, records);
                        self.endPulldownToRefresh();
                        self.refresh(true);

                    });
                }
            },
            up: {
                contentinit: dfop.up.contentinit,
                contentdown: dfop.up.contentdown,
                contentrefresh: dfop.up.contentrefresh,
                contentnomore: dfop.up.contentnomore,
                callback: function () {
                    var self = this;
                    var op = self.options;
                    op.lrpage = op.lrpage + 1;
                    var pageparam = {
                        page: op.lrpage,
                        rows: op.lrrows
                    };
                    if (op.lrpage > op.lrtotal) {
                        self.endPullupToRefresh(true);
                    }
                    else {
                        op.lrgetData && op.lrgetData(pageparam, function (data, records) {
                            _lrpagination.renderData(op, data, op.lrpage, records);
                            if (op.lrpage >= op.lrtotal) {
                                self.endPullupToRefresh(true);
                            }
                            else {
                                self.endPullupToRefresh();
                            }
                        });
                    }
                }
            }
        };

        var $res = $this.pullRefresh(fop);
        var $list = $('<div class="' + dfop.lclass + '" ></div>');
        $($res.wrapper.children[1]).prepend($list);
        $list.html("");
        fui.loading(true, '加载数据中');

        var _fop = $res.options;
        _fop.$list = $list;

        $list.delegate('.lr-page-item', 'tap', { op: _fop }, function (e) {
            e = e || window.event;
            var et = e.target || e.srcElement;
            var $et = $(et);

            var op = e.data.op;
            var data = op.lrdata;
            var $this = $(this);
            var _index = $this.attr('data-index');
            op.lrclick && op.lrclick(data[_index], $this, $et);
        });

        var pageparam = {
            page: 1,
            rows: _fop.lrrows
        };
        _fop.lrgetData && _fop.lrgetData(pageparam, function (data, records) {
            $list.html("");
            _fop.lrdata = [];
            _lrpagination.renderData(_fop, data, 1, records);
            fui.loading(false);
            _fop = null;
        });
        fop = null;
        dfop = null;
        op = null;
        $res.reload = _lrpagination.reload;
        return $res;
    };

})(window.jQuery, window.lrmui, window.fui, window);


/*
 * 版 本 Learun-Mobile V2.0.0 力软敏捷开发框架(http://www.learun.cn)
 * Copyright (c) 2013-2018 上海力软信息技术有限公司
 * 创建人：力软-前端开发组
 * 日 期：2017.12.15
 * 描 述：力软移动端框架 启动屏
 */
(function ($, learun, fui, window) {
    "use strict";

    learun.splashscreen = {
        hide: function () {
            learun.isreal && navigator.splashscreen && navigator.splashscreen.hide();
        },
        show: function () {
            learun.isreal && navigator.splashscreen && navigator.splashscreen.show();
        }
    };

})(window.jQuery, window.lrmui, window.fui, window);/**
 * @fileoverview
 * - Using the 'QRCode for Javascript library'
 * - Fixed dataset of 'QRCode for Javascript library' for support full-spec.
 * - this library has no dependencies.
 * 
 * @author davidshimjs
 * @see <a href="http://www.d-project.com/" target="_blank">http://www.d-project.com/</a>
 * @see <a href="http://jeromeetienne.github.com/jquery-qrcode/" target="_blank">http://jeromeetienne.github.com/jquery-qrcode/</a>
 */
var QRCode;

(function () {
    //---------------------------------------------------------------------
    // QRCode for JavaScript
    //
    // Copyright (c) 2009 Kazuhiko Arase
    //
    // URL: http://www.d-project.com/
    //
    // Licensed under the MIT license:
    //   http://www.opensource.org/licenses/mit-license.php
    //
    // The word "QR Code" is registered trademark of 
    // DENSO WAVE INCORPORATED
    //   http://www.denso-wave.com/qrcode/faqpatent-e.html
    //
    //---------------------------------------------------------------------
    function QR8bitByte(data) {
        this.mode = QRMode.MODE_8BIT_BYTE;
        this.data = data;
        this.parsedData = [];

        // Added to support UTF-8 Characters
        for (var i = 0, l = this.data.length; i < l; i++) {
            var byteArray = [];
            var code = this.data.charCodeAt(i);

            if (code > 0x10000) {
                byteArray[0] = 0xF0 | ((code & 0x1C0000) >>> 18);
                byteArray[1] = 0x80 | ((code & 0x3F000) >>> 12);
                byteArray[2] = 0x80 | ((code & 0xFC0) >>> 6);
                byteArray[3] = 0x80 | (code & 0x3F);
            } else if (code > 0x800) {
                byteArray[0] = 0xE0 | ((code & 0xF000) >>> 12);
                byteArray[1] = 0x80 | ((code & 0xFC0) >>> 6);
                byteArray[2] = 0x80 | (code & 0x3F);
            } else if (code > 0x80) {
                byteArray[0] = 0xC0 | ((code & 0x7C0) >>> 6);
                byteArray[1] = 0x80 | (code & 0x3F);
            } else {
                byteArray[0] = code;
            }

            this.parsedData.push(byteArray);
        }

        this.parsedData = Array.prototype.concat.apply([], this.parsedData);

        if (this.parsedData.length != this.data.length) {
            this.parsedData.unshift(191);
            this.parsedData.unshift(187);
            this.parsedData.unshift(239);
        }
    }

    QR8bitByte.prototype = {
        getLength: function (buffer) {
            return this.parsedData.length;
        },
        write: function (buffer) {
            for (var i = 0, l = this.parsedData.length; i < l; i++) {
                buffer.put(this.parsedData[i], 8);
            }
        }
    };

    function QRCodeModel(typeNumber, errorCorrectLevel) {
        this.typeNumber = typeNumber;
        this.errorCorrectLevel = errorCorrectLevel;
        this.modules = null;
        this.moduleCount = 0;
        this.dataCache = null;
        this.dataList = [];
    }

    QRCodeModel.prototype = {
        addData: function (data) { var newData = new QR8bitByte(data); this.dataList.push(newData); this.dataCache = null; }, isDark: function (row, col) {
            if (row < 0 || this.moduleCount <= row || col < 0 || this.moduleCount <= col) { throw new Error(row + "," + col); }
            return this.modules[row][col];
        }, getModuleCount: function () { return this.moduleCount; }, make: function () { this.makeImpl(false, this.getBestMaskPattern()); }, makeImpl: function (test, maskPattern) {
        this.moduleCount = this.typeNumber * 4 + 17; this.modules = new Array(this.moduleCount); for (var row = 0; row < this.moduleCount; row++) { this.modules[row] = new Array(this.moduleCount); for (var col = 0; col < this.moduleCount; col++) { this.modules[row][col] = null; } }
            this.setupPositionProbePattern(0, 0); this.setupPositionProbePattern(this.moduleCount - 7, 0); this.setupPositionProbePattern(0, this.moduleCount - 7); this.setupPositionAdjustPattern(); this.setupTimingPattern(); this.setupTypeInfo(test, maskPattern); if (this.typeNumber >= 7) { this.setupTypeNumber(test); }
            if (this.dataCache == null) { this.dataCache = QRCodeModel.createData(this.typeNumber, this.errorCorrectLevel, this.dataList); }
            this.mapData(this.dataCache, maskPattern);
        }, setupPositionProbePattern: function (row, col) { for (var r = -1; r <= 7; r++) { if (row + r <= -1 || this.moduleCount <= row + r) continue; for (var c = -1; c <= 7; c++) { if (col + c <= -1 || this.moduleCount <= col + c) continue; if ((0 <= r && r <= 6 && (c == 0 || c == 6)) || (0 <= c && c <= 6 && (r == 0 || r == 6)) || (2 <= r && r <= 4 && 2 <= c && c <= 4)) { this.modules[row + r][col + c] = true; } else { this.modules[row + r][col + c] = false; } } } }, getBestMaskPattern: function () {
            var minLostPoint = 0; var pattern = 0; for (var i = 0; i < 8; i++) { this.makeImpl(true, i); var lostPoint = QRUtil.getLostPoint(this); if (i == 0 || minLostPoint > lostPoint) { minLostPoint = lostPoint; pattern = i; } }
            return pattern;
        }, createMovieClip: function (target_mc, instance_name, depth) {
            var qr_mc = target_mc.createEmptyMovieClip(instance_name, depth); var cs = 1; this.make(); for (var row = 0; row < this.modules.length; row++) { var y = row * cs; for (var col = 0; col < this.modules[row].length; col++) { var x = col * cs; var dark = this.modules[row][col]; if (dark) { qr_mc.beginFill(0, 100); qr_mc.moveTo(x, y); qr_mc.lineTo(x + cs, y); qr_mc.lineTo(x + cs, y + cs); qr_mc.lineTo(x, y + cs); qr_mc.endFill(); } } }
            return qr_mc;
        }, setupTimingPattern: function () {
            for (var r = 8; r < this.moduleCount - 8; r++) {
                if (this.modules[r][6] != null) { continue; }
                this.modules[r][6] = (r % 2 == 0);
            }
            for (var c = 8; c < this.moduleCount - 8; c++) {
                if (this.modules[6][c] != null) { continue; }
                this.modules[6][c] = (c % 2 == 0);
            }
        }, setupPositionAdjustPattern: function () {
            var pos = QRUtil.getPatternPosition(this.typeNumber); for (var i = 0; i < pos.length; i++) {
                for (var j = 0; j < pos.length; j++) {
                    var row = pos[i]; var col = pos[j]; if (this.modules[row][col] != null) { continue; }
                    for (var r = -2; r <= 2; r++) { for (var c = -2; c <= 2; c++) { if (r == -2 || r == 2 || c == -2 || c == 2 || (r == 0 && c == 0)) { this.modules[row + r][col + c] = true; } else { this.modules[row + r][col + c] = false; } } }
                }
            }
        }, setupTypeNumber: function (test) {
            var bits = QRUtil.getBCHTypeNumber(this.typeNumber); for (var i = 0; i < 18; i++) { var mod = (!test && ((bits >> i) & 1) == 1); this.modules[Math.floor(i / 3)][i % 3 + this.moduleCount - 8 - 3] = mod; }
            for (var i = 0; i < 18; i++) { var mod = (!test && ((bits >> i) & 1) == 1); this.modules[i % 3 + this.moduleCount - 8 - 3][Math.floor(i / 3)] = mod; }
        }, setupTypeInfo: function (test, maskPattern) {
            var data = (this.errorCorrectLevel << 3) | maskPattern; var bits = QRUtil.getBCHTypeInfo(data); for (var i = 0; i < 15; i++) { var mod = (!test && ((bits >> i) & 1) == 1); if (i < 6) { this.modules[i][8] = mod; } else if (i < 8) { this.modules[i + 1][8] = mod; } else { this.modules[this.moduleCount - 15 + i][8] = mod; } }
            for (var i = 0; i < 15; i++) { var mod = (!test && ((bits >> i) & 1) == 1); if (i < 8) { this.modules[8][this.moduleCount - i - 1] = mod; } else if (i < 9) { this.modules[8][15 - i - 1 + 1] = mod; } else { this.modules[8][15 - i - 1] = mod; } }
            this.modules[this.moduleCount - 8][8] = (!test);
        }, mapData: function (data, maskPattern) {
            var inc = -1; var row = this.moduleCount - 1; var bitIndex = 7; var byteIndex = 0; for (var col = this.moduleCount - 1; col > 0; col -= 2) {
                if (col == 6) col--; while (true) {
                    for (var c = 0; c < 2; c++) {
                        if (this.modules[row][col - c] == null) {
                            var dark = false; if (byteIndex < data.length) { dark = (((data[byteIndex] >>> bitIndex) & 1) == 1); }
                            var mask = QRUtil.getMask(maskPattern, row, col - c); if (mask) { dark = !dark; }
                            this.modules[row][col - c] = dark; bitIndex--; if (bitIndex == -1) { byteIndex++; bitIndex = 7; }
                        }
                    }
                    row += inc; if (row < 0 || this.moduleCount <= row) { row -= inc; inc = -inc; break; }
                }
            }
        }
    }; QRCodeModel.PAD0 = 0xEC; QRCodeModel.PAD1 = 0x11; QRCodeModel.createData = function (typeNumber, errorCorrectLevel, dataList) {
        var rsBlocks = QRRSBlock.getRSBlocks(typeNumber, errorCorrectLevel); var buffer = new QRBitBuffer(); for (var i = 0; i < dataList.length; i++) { var data = dataList[i]; buffer.put(data.mode, 4); buffer.put(data.getLength(), QRUtil.getLengthInBits(data.mode, typeNumber)); data.write(buffer); }
        var totalDataCount = 0; for (var i = 0; i < rsBlocks.length; i++) { totalDataCount += rsBlocks[i].dataCount; }
        if (buffer.getLengthInBits() > totalDataCount * 8) {
            throw new Error("code length overflow. ("
                + buffer.getLengthInBits()
                + ">"
                + totalDataCount * 8
                + ")");
        }
        if (buffer.getLengthInBits() + 4 <= totalDataCount * 8) { buffer.put(0, 4); }
        while (buffer.getLengthInBits() % 8 != 0) { buffer.putBit(false); }
        while (true) {
            if (buffer.getLengthInBits() >= totalDataCount * 8) { break; }
            buffer.put(QRCodeModel.PAD0, 8); if (buffer.getLengthInBits() >= totalDataCount * 8) { break; }
            buffer.put(QRCodeModel.PAD1, 8);
        }
        return QRCodeModel.createBytes(buffer, rsBlocks);
    }; QRCodeModel.createBytes = function (buffer, rsBlocks) {
        var offset = 0; var maxDcCount = 0; var maxEcCount = 0; var dcdata = new Array(rsBlocks.length); var ecdata = new Array(rsBlocks.length); for (var r = 0; r < rsBlocks.length; r++) {
            var dcCount = rsBlocks[r].dataCount; var ecCount = rsBlocks[r].totalCount - dcCount; maxDcCount = Math.max(maxDcCount, dcCount); maxEcCount = Math.max(maxEcCount, ecCount); dcdata[r] = new Array(dcCount); for (var i = 0; i < dcdata[r].length; i++) { dcdata[r][i] = 0xff & buffer.buffer[i + offset]; }
            offset += dcCount; var rsPoly = QRUtil.getErrorCorrectPolynomial(ecCount); var rawPoly = new QRPolynomial(dcdata[r], rsPoly.getLength() - 1); var modPoly = rawPoly.mod(rsPoly); ecdata[r] = new Array(rsPoly.getLength() - 1); for (var i = 0; i < ecdata[r].length; i++) { var modIndex = i + modPoly.getLength() - ecdata[r].length; ecdata[r][i] = (modIndex >= 0) ? modPoly.get(modIndex) : 0; }
        }
        var totalCodeCount = 0; for (var i = 0; i < rsBlocks.length; i++) { totalCodeCount += rsBlocks[i].totalCount; }
        var data = new Array(totalCodeCount); var index = 0; for (var i = 0; i < maxDcCount; i++) { for (var r = 0; r < rsBlocks.length; r++) { if (i < dcdata[r].length) { data[index++] = dcdata[r][i]; } } }
        for (var i = 0; i < maxEcCount; i++) { for (var r = 0; r < rsBlocks.length; r++) { if (i < ecdata[r].length) { data[index++] = ecdata[r][i]; } } }
        return data;
    }; var QRMode = { MODE_NUMBER: 1 << 0, MODE_ALPHA_NUM: 1 << 1, MODE_8BIT_BYTE: 1 << 2, MODE_KANJI: 1 << 3 }; var QRErrorCorrectLevel = { L: 1, M: 0, Q: 3, H: 2 }; var QRMaskPattern = { PATTERN000: 0, PATTERN001: 1, PATTERN010: 2, PATTERN011: 3, PATTERN100: 4, PATTERN101: 5, PATTERN110: 6, PATTERN111: 7 }; var QRUtil = {
        PATTERN_POSITION_TABLE: [[], [6, 18], [6, 22], [6, 26], [6, 30], [6, 34], [6, 22, 38], [6, 24, 42], [6, 26, 46], [6, 28, 50], [6, 30, 54], [6, 32, 58], [6, 34, 62], [6, 26, 46, 66], [6, 26, 48, 70], [6, 26, 50, 74], [6, 30, 54, 78], [6, 30, 56, 82], [6, 30, 58, 86], [6, 34, 62, 90], [6, 28, 50, 72, 94], [6, 26, 50, 74, 98], [6, 30, 54, 78, 102], [6, 28, 54, 80, 106], [6, 32, 58, 84, 110], [6, 30, 58, 86, 114], [6, 34, 62, 90, 118], [6, 26, 50, 74, 98, 122], [6, 30, 54, 78, 102, 126], [6, 26, 52, 78, 104, 130], [6, 30, 56, 82, 108, 134], [6, 34, 60, 86, 112, 138], [6, 30, 58, 86, 114, 142], [6, 34, 62, 90, 118, 146], [6, 30, 54, 78, 102, 126, 150], [6, 24, 50, 76, 102, 128, 154], [6, 28, 54, 80, 106, 132, 158], [6, 32, 58, 84, 110, 136, 162], [6, 26, 54, 82, 110, 138, 166], [6, 30, 58, 86, 114, 142, 170]], G15: (1 << 10) | (1 << 8) | (1 << 5) | (1 << 4) | (1 << 2) | (1 << 1) | (1 << 0), G18: (1 << 12) | (1 << 11) | (1 << 10) | (1 << 9) | (1 << 8) | (1 << 5) | (1 << 2) | (1 << 0), G15_MASK: (1 << 14) | (1 << 12) | (1 << 10) | (1 << 4) | (1 << 1), getBCHTypeInfo: function (data) {
            var d = data << 10; while (QRUtil.getBCHDigit(d) - QRUtil.getBCHDigit(QRUtil.G15) >= 0) { d ^= (QRUtil.G15 << (QRUtil.getBCHDigit(d) - QRUtil.getBCHDigit(QRUtil.G15))); }
            return ((data << 10) | d) ^ QRUtil.G15_MASK;
        }, getBCHTypeNumber: function (data) {
            var d = data << 12; while (QRUtil.getBCHDigit(d) - QRUtil.getBCHDigit(QRUtil.G18) >= 0) { d ^= (QRUtil.G18 << (QRUtil.getBCHDigit(d) - QRUtil.getBCHDigit(QRUtil.G18))); }
            return (data << 12) | d;
        }, getBCHDigit: function (data) {
            var digit = 0; while (data != 0) { digit++; data >>>= 1; }
            return digit;
        }, getPatternPosition: function (typeNumber) { return QRUtil.PATTERN_POSITION_TABLE[typeNumber - 1]; }, getMask: function (maskPattern, i, j) { switch (maskPattern) { case QRMaskPattern.PATTERN000: return (i + j) % 2 == 0; case QRMaskPattern.PATTERN001: return i % 2 == 0; case QRMaskPattern.PATTERN010: return j % 3 == 0; case QRMaskPattern.PATTERN011: return (i + j) % 3 == 0; case QRMaskPattern.PATTERN100: return (Math.floor(i / 2) + Math.floor(j / 3)) % 2 == 0; case QRMaskPattern.PATTERN101: return (i * j) % 2 + (i * j) % 3 == 0; case QRMaskPattern.PATTERN110: return ((i * j) % 2 + (i * j) % 3) % 2 == 0; case QRMaskPattern.PATTERN111: return ((i * j) % 3 + (i + j) % 2) % 2 == 0; default: throw new Error("bad maskPattern:" + maskPattern); } }, getErrorCorrectPolynomial: function (errorCorrectLength) {
            var a = new QRPolynomial([1], 0); for (var i = 0; i < errorCorrectLength; i++) { a = a.multiply(new QRPolynomial([1, QRMath.gexp(i)], 0)); }
            return a;
        }, getLengthInBits: function (mode, type) { if (1 <= type && type < 10) { switch (mode) { case QRMode.MODE_NUMBER: return 10; case QRMode.MODE_ALPHA_NUM: return 9; case QRMode.MODE_8BIT_BYTE: return 8; case QRMode.MODE_KANJI: return 8; default: throw new Error("mode:" + mode); } } else if (type < 27) { switch (mode) { case QRMode.MODE_NUMBER: return 12; case QRMode.MODE_ALPHA_NUM: return 11; case QRMode.MODE_8BIT_BYTE: return 16; case QRMode.MODE_KANJI: return 10; default: throw new Error("mode:" + mode); } } else if (type < 41) { switch (mode) { case QRMode.MODE_NUMBER: return 14; case QRMode.MODE_ALPHA_NUM: return 13; case QRMode.MODE_8BIT_BYTE: return 16; case QRMode.MODE_KANJI: return 12; default: throw new Error("mode:" + mode); } } else { throw new Error("type:" + type); } }, getLostPoint: function (qrCode) {
            var moduleCount = qrCode.getModuleCount(); var lostPoint = 0; for (var row = 0; row < moduleCount; row++) {
                for (var col = 0; col < moduleCount; col++) {
                    var sameCount = 0; var dark = qrCode.isDark(row, col); for (var r = -1; r <= 1; r++) {
                        if (row + r < 0 || moduleCount <= row + r) { continue; }
                        for (var c = -1; c <= 1; c++) {
                            if (col + c < 0 || moduleCount <= col + c) { continue; }
                            if (r == 0 && c == 0) { continue; }
                            if (dark == qrCode.isDark(row + r, col + c)) { sameCount++; }
                        }
                    }
                    if (sameCount > 5) { lostPoint += (3 + sameCount - 5); }
                }
            }
            for (var row = 0; row < moduleCount - 1; row++) { for (var col = 0; col < moduleCount - 1; col++) { var count = 0; if (qrCode.isDark(row, col)) count++; if (qrCode.isDark(row + 1, col)) count++; if (qrCode.isDark(row, col + 1)) count++; if (qrCode.isDark(row + 1, col + 1)) count++; if (count == 0 || count == 4) { lostPoint += 3; } } }
            for (var row = 0; row < moduleCount; row++) { for (var col = 0; col < moduleCount - 6; col++) { if (qrCode.isDark(row, col) && !qrCode.isDark(row, col + 1) && qrCode.isDark(row, col + 2) && qrCode.isDark(row, col + 3) && qrCode.isDark(row, col + 4) && !qrCode.isDark(row, col + 5) && qrCode.isDark(row, col + 6)) { lostPoint += 40; } } }
            for (var col = 0; col < moduleCount; col++) { for (var row = 0; row < moduleCount - 6; row++) { if (qrCode.isDark(row, col) && !qrCode.isDark(row + 1, col) && qrCode.isDark(row + 2, col) && qrCode.isDark(row + 3, col) && qrCode.isDark(row + 4, col) && !qrCode.isDark(row + 5, col) && qrCode.isDark(row + 6, col)) { lostPoint += 40; } } }
            var darkCount = 0; for (var col = 0; col < moduleCount; col++) { for (var row = 0; row < moduleCount; row++) { if (qrCode.isDark(row, col)) { darkCount++; } } }
            var ratio = Math.abs(100 * darkCount / moduleCount / moduleCount - 50) / 5; lostPoint += ratio * 10; return lostPoint;
        }
    }; var QRMath = {
        glog: function (n) {
            if (n < 1) { throw new Error("glog(" + n + ")"); }
            return QRMath.LOG_TABLE[n];
        }, gexp: function (n) {
            while (n < 0) { n += 255; }
            while (n >= 256) { n -= 255; }
            return QRMath.EXP_TABLE[n];
        }, EXP_TABLE: new Array(256), LOG_TABLE: new Array(256)
    }; for (var i = 0; i < 8; i++) { QRMath.EXP_TABLE[i] = 1 << i; }
    for (var i = 8; i < 256; i++) { QRMath.EXP_TABLE[i] = QRMath.EXP_TABLE[i - 4] ^ QRMath.EXP_TABLE[i - 5] ^ QRMath.EXP_TABLE[i - 6] ^ QRMath.EXP_TABLE[i - 8]; }
    for (var i = 0; i < 255; i++) { QRMath.LOG_TABLE[QRMath.EXP_TABLE[i]] = i; }
    function QRPolynomial(num, shift) {
        if (num.length == undefined) { throw new Error(num.length + "/" + shift); }
        var offset = 0; while (offset < num.length && num[offset] == 0) { offset++; }
        this.num = new Array(num.length - offset + shift); for (var i = 0; i < num.length - offset; i++) { this.num[i] = num[i + offset]; }
    }
    QRPolynomial.prototype = {
        get: function (index) { return this.num[index]; }, getLength: function () { return this.num.length; }, multiply: function (e) {
            var num = new Array(this.getLength() + e.getLength() - 1); for (var i = 0; i < this.getLength(); i++) { for (var j = 0; j < e.getLength(); j++) { num[i + j] ^= QRMath.gexp(QRMath.glog(this.get(i)) + QRMath.glog(e.get(j))); } }
            return new QRPolynomial(num, 0);
        }, mod: function (e) {
            if (this.getLength() - e.getLength() < 0) { return this; }
            var ratio = QRMath.glog(this.get(0)) - QRMath.glog(e.get(0)); var num = new Array(this.getLength()); for (var i = 0; i < this.getLength(); i++) { num[i] = this.get(i); }
            for (var i = 0; i < e.getLength(); i++) { num[i] ^= QRMath.gexp(QRMath.glog(e.get(i)) + ratio); }
            return new QRPolynomial(num, 0).mod(e);
        }
    }; function QRRSBlock(totalCount, dataCount) { this.totalCount = totalCount; this.dataCount = dataCount; }
    QRRSBlock.RS_BLOCK_TABLE = [[1, 26, 19], [1, 26, 16], [1, 26, 13], [1, 26, 9], [1, 44, 34], [1, 44, 28], [1, 44, 22], [1, 44, 16], [1, 70, 55], [1, 70, 44], [2, 35, 17], [2, 35, 13], [1, 100, 80], [2, 50, 32], [2, 50, 24], [4, 25, 9], [1, 134, 108], [2, 67, 43], [2, 33, 15, 2, 34, 16], [2, 33, 11, 2, 34, 12], [2, 86, 68], [4, 43, 27], [4, 43, 19], [4, 43, 15], [2, 98, 78], [4, 49, 31], [2, 32, 14, 4, 33, 15], [4, 39, 13, 1, 40, 14], [2, 121, 97], [2, 60, 38, 2, 61, 39], [4, 40, 18, 2, 41, 19], [4, 40, 14, 2, 41, 15], [2, 146, 116], [3, 58, 36, 2, 59, 37], [4, 36, 16, 4, 37, 17], [4, 36, 12, 4, 37, 13], [2, 86, 68, 2, 87, 69], [4, 69, 43, 1, 70, 44], [6, 43, 19, 2, 44, 20], [6, 43, 15, 2, 44, 16], [4, 101, 81], [1, 80, 50, 4, 81, 51], [4, 50, 22, 4, 51, 23], [3, 36, 12, 8, 37, 13], [2, 116, 92, 2, 117, 93], [6, 58, 36, 2, 59, 37], [4, 46, 20, 6, 47, 21], [7, 42, 14, 4, 43, 15], [4, 133, 107], [8, 59, 37, 1, 60, 38], [8, 44, 20, 4, 45, 21], [12, 33, 11, 4, 34, 12], [3, 145, 115, 1, 146, 116], [4, 64, 40, 5, 65, 41], [11, 36, 16, 5, 37, 17], [11, 36, 12, 5, 37, 13], [5, 109, 87, 1, 110, 88], [5, 65, 41, 5, 66, 42], [5, 54, 24, 7, 55, 25], [11, 36, 12], [5, 122, 98, 1, 123, 99], [7, 73, 45, 3, 74, 46], [15, 43, 19, 2, 44, 20], [3, 45, 15, 13, 46, 16], [1, 135, 107, 5, 136, 108], [10, 74, 46, 1, 75, 47], [1, 50, 22, 15, 51, 23], [2, 42, 14, 17, 43, 15], [5, 150, 120, 1, 151, 121], [9, 69, 43, 4, 70, 44], [17, 50, 22, 1, 51, 23], [2, 42, 14, 19, 43, 15], [3, 141, 113, 4, 142, 114], [3, 70, 44, 11, 71, 45], [17, 47, 21, 4, 48, 22], [9, 39, 13, 16, 40, 14], [3, 135, 107, 5, 136, 108], [3, 67, 41, 13, 68, 42], [15, 54, 24, 5, 55, 25], [15, 43, 15, 10, 44, 16], [4, 144, 116, 4, 145, 117], [17, 68, 42], [17, 50, 22, 6, 51, 23], [19, 46, 16, 6, 47, 17], [2, 139, 111, 7, 140, 112], [17, 74, 46], [7, 54, 24, 16, 55, 25], [34, 37, 13], [4, 151, 121, 5, 152, 122], [4, 75, 47, 14, 76, 48], [11, 54, 24, 14, 55, 25], [16, 45, 15, 14, 46, 16], [6, 147, 117, 4, 148, 118], [6, 73, 45, 14, 74, 46], [11, 54, 24, 16, 55, 25], [30, 46, 16, 2, 47, 17], [8, 132, 106, 4, 133, 107], [8, 75, 47, 13, 76, 48], [7, 54, 24, 22, 55, 25], [22, 45, 15, 13, 46, 16], [10, 142, 114, 2, 143, 115], [19, 74, 46, 4, 75, 47], [28, 50, 22, 6, 51, 23], [33, 46, 16, 4, 47, 17], [8, 152, 122, 4, 153, 123], [22, 73, 45, 3, 74, 46], [8, 53, 23, 26, 54, 24], [12, 45, 15, 28, 46, 16], [3, 147, 117, 10, 148, 118], [3, 73, 45, 23, 74, 46], [4, 54, 24, 31, 55, 25], [11, 45, 15, 31, 46, 16], [7, 146, 116, 7, 147, 117], [21, 73, 45, 7, 74, 46], [1, 53, 23, 37, 54, 24], [19, 45, 15, 26, 46, 16], [5, 145, 115, 10, 146, 116], [19, 75, 47, 10, 76, 48], [15, 54, 24, 25, 55, 25], [23, 45, 15, 25, 46, 16], [13, 145, 115, 3, 146, 116], [2, 74, 46, 29, 75, 47], [42, 54, 24, 1, 55, 25], [23, 45, 15, 28, 46, 16], [17, 145, 115], [10, 74, 46, 23, 75, 47], [10, 54, 24, 35, 55, 25], [19, 45, 15, 35, 46, 16], [17, 145, 115, 1, 146, 116], [14, 74, 46, 21, 75, 47], [29, 54, 24, 19, 55, 25], [11, 45, 15, 46, 46, 16], [13, 145, 115, 6, 146, 116], [14, 74, 46, 23, 75, 47], [44, 54, 24, 7, 55, 25], [59, 46, 16, 1, 47, 17], [12, 151, 121, 7, 152, 122], [12, 75, 47, 26, 76, 48], [39, 54, 24, 14, 55, 25], [22, 45, 15, 41, 46, 16], [6, 151, 121, 14, 152, 122], [6, 75, 47, 34, 76, 48], [46, 54, 24, 10, 55, 25], [2, 45, 15, 64, 46, 16], [17, 152, 122, 4, 153, 123], [29, 74, 46, 14, 75, 47], [49, 54, 24, 10, 55, 25], [24, 45, 15, 46, 46, 16], [4, 152, 122, 18, 153, 123], [13, 74, 46, 32, 75, 47], [48, 54, 24, 14, 55, 25], [42, 45, 15, 32, 46, 16], [20, 147, 117, 4, 148, 118], [40, 75, 47, 7, 76, 48], [43, 54, 24, 22, 55, 25], [10, 45, 15, 67, 46, 16], [19, 148, 118, 6, 149, 119], [18, 75, 47, 31, 76, 48], [34, 54, 24, 34, 55, 25], [20, 45, 15, 61, 46, 16]]; QRRSBlock.getRSBlocks = function (typeNumber, errorCorrectLevel) {
        var rsBlock = QRRSBlock.getRsBlockTable(typeNumber, errorCorrectLevel); if (rsBlock == undefined) { throw new Error("bad rs block @ typeNumber:" + typeNumber + "/errorCorrectLevel:" + errorCorrectLevel); }
        var length = rsBlock.length / 3; var list = []; for (var i = 0; i < length; i++) { var count = rsBlock[i * 3 + 0]; var totalCount = rsBlock[i * 3 + 1]; var dataCount = rsBlock[i * 3 + 2]; for (var j = 0; j < count; j++) { list.push(new QRRSBlock(totalCount, dataCount)); } }
        return list;
    }; QRRSBlock.getRsBlockTable = function (typeNumber, errorCorrectLevel) { switch (errorCorrectLevel) { case QRErrorCorrectLevel.L: return QRRSBlock.RS_BLOCK_TABLE[(typeNumber - 1) * 4 + 0]; case QRErrorCorrectLevel.M: return QRRSBlock.RS_BLOCK_TABLE[(typeNumber - 1) * 4 + 1]; case QRErrorCorrectLevel.Q: return QRRSBlock.RS_BLOCK_TABLE[(typeNumber - 1) * 4 + 2]; case QRErrorCorrectLevel.H: return QRRSBlock.RS_BLOCK_TABLE[(typeNumber - 1) * 4 + 3]; default: return undefined; } }; function QRBitBuffer() { this.buffer = []; this.length = 0; }
    QRBitBuffer.prototype = {
        get: function (index) { var bufIndex = Math.floor(index / 8); return ((this.buffer[bufIndex] >>> (7 - index % 8)) & 1) == 1; }, put: function (num, length) { for (var i = 0; i < length; i++) { this.putBit(((num >>> (length - i - 1)) & 1) == 1); } }, getLengthInBits: function () { return this.length; }, putBit: function (bit) {
            var bufIndex = Math.floor(this.length / 8); if (this.buffer.length <= bufIndex) { this.buffer.push(0); }
            if (bit) { this.buffer[bufIndex] |= (0x80 >>> (this.length % 8)); }
            this.length++;
        }
    }; var QRCodeLimitLength = [[17, 14, 11, 7], [32, 26, 20, 14], [53, 42, 32, 24], [78, 62, 46, 34], [106, 84, 60, 44], [134, 106, 74, 58], [154, 122, 86, 64], [192, 152, 108, 84], [230, 180, 130, 98], [271, 213, 151, 119], [321, 251, 177, 137], [367, 287, 203, 155], [425, 331, 241, 177], [458, 362, 258, 194], [520, 412, 292, 220], [586, 450, 322, 250], [644, 504, 364, 280], [718, 560, 394, 310], [792, 624, 442, 338], [858, 666, 482, 382], [929, 711, 509, 403], [1003, 779, 565, 439], [1091, 857, 611, 461], [1171, 911, 661, 511], [1273, 997, 715, 535], [1367, 1059, 751, 593], [1465, 1125, 805, 625], [1528, 1190, 868, 658], [1628, 1264, 908, 698], [1732, 1370, 982, 742], [1840, 1452, 1030, 790], [1952, 1538, 1112, 842], [2068, 1628, 1168, 898], [2188, 1722, 1228, 958], [2303, 1809, 1283, 983], [2431, 1911, 1351, 1051], [2563, 1989, 1423, 1093], [2699, 2099, 1499, 1139], [2809, 2213, 1579, 1219], [2953, 2331, 1663, 1273]];

    function _isSupportCanvas() {
        return typeof CanvasRenderingContext2D != "undefined";
    }

    // android 2.x doesn't support Data-URI spec
    function _getAndroid() {
        var android = false;
        var sAgent = navigator.userAgent;

        if (/android/i.test(sAgent)) { // android
            android = true;
            var aMat = sAgent.toString().match(/android ([0-9]\.[0-9])/i);

            if (aMat && aMat[1]) {
                android = parseFloat(aMat[1]);
            }
        }

        return android;
    }

    var svgDrawer = (function () {

        var Drawing = function (el, htOption) {
            this._el = el;
            this._htOption = htOption;
        };

        Drawing.prototype.draw = function (oQRCode) {
            var _htOption = this._htOption;
            var _el = this._el;
            var nCount = oQRCode.getModuleCount();
            var nWidth = Math.floor(_htOption.width / nCount);
            var nHeight = Math.floor(_htOption.height / nCount);

            this.clear();

            function makeSVG(tag, attrs) {
                var el = document.createElementNS('http://www.w3.org/2000/svg', tag);
                for (var k in attrs)
                    if (attrs.hasOwnProperty(k)) el.setAttribute(k, attrs[k]);
                return el;
            }

            var svg = makeSVG("svg", { 'viewBox': '0 0 ' + String(nCount) + " " + String(nCount), 'width': '100%', 'height': '100%', 'fill': _htOption.colorLight });
            svg.setAttributeNS("http://www.w3.org/2000/xmlns/", "xmlns:xlink", "http://www.w3.org/1999/xlink");
            _el.appendChild(svg);

            svg.appendChild(makeSVG("rect", { "fill": _htOption.colorLight, "width": "100%", "height": "100%" }));
            svg.appendChild(makeSVG("rect", { "fill": _htOption.colorDark, "width": "1", "height": "1", "id": "template" }));

            for (var row = 0; row < nCount; row++) {
                for (var col = 0; col < nCount; col++) {
                    if (oQRCode.isDark(row, col)) {
                        var child = makeSVG("use", { "x": String(col), "y": String(row) });
                        child.setAttributeNS("http://www.w3.org/1999/xlink", "href", "#template")
                        svg.appendChild(child);
                    }
                }
            }
        };
        Drawing.prototype.clear = function () {
            while (this._el.hasChildNodes())
                this._el.removeChild(this._el.lastChild);
        };
        return Drawing;
    })();

    var useSVG = document.documentElement.tagName.toLowerCase() === "svg";

    // Drawing in DOM by using Table tag
    var Drawing = useSVG ? svgDrawer : !_isSupportCanvas() ? (function () {
        var Drawing = function (el, htOption) {
            this._el = el;
            this._htOption = htOption;
        };

		/**
		 * Draw the QRCode
		 * 
		 * @param {QRCode} oQRCode
		 */
        Drawing.prototype.draw = function (oQRCode) {
            var _htOption = this._htOption;
            var _el = this._el;
            var nCount = oQRCode.getModuleCount();
            var nWidth = Math.floor(_htOption.width / nCount);
            var nHeight = Math.floor(_htOption.height / nCount);
            var aHTML = ['<table style="border:0;border-collapse:collapse;">'];

            for (var row = 0; row < nCount; row++) {
                aHTML.push('<tr>');

                for (var col = 0; col < nCount; col++) {
                    aHTML.push('<td style="border:0;border-collapse:collapse;padding:0;margin:0;width:' + nWidth + 'px;height:' + nHeight + 'px;background-color:' + (oQRCode.isDark(row, col) ? _htOption.colorDark : _htOption.colorLight) + ';"></td>');
                }

                aHTML.push('</tr>');
            }

            aHTML.push('</table>');
            _el.innerHTML = aHTML.join('');

            // Fix the margin values as real size.
            var elTable = _el.childNodes[0];
            var nLeftMarginTable = (_htOption.width - elTable.offsetWidth) / 2;
            var nTopMarginTable = (_htOption.height - elTable.offsetHeight) / 2;

            if (nLeftMarginTable > 0 && nTopMarginTable > 0) {
                elTable.style.margin = nTopMarginTable + "px " + nLeftMarginTable + "px";
            }
        };

		/**
		 * Clear the QRCode
		 */
        Drawing.prototype.clear = function () {
            this._el.innerHTML = '';
        };

        return Drawing;
    })() : (function () { // Drawing in Canvas
        function _onMakeImage() {
            this._elImage.src = this._elCanvas.toDataURL("image/png");
            this._elImage.style.display = "block";
            this._elCanvas.style.display = "none";
        }

        // Android 2.1 bug workaround
        // http://code.google.com/p/android/issues/detail?id=5141
        if (this._android && this._android <= 2.1) {
            var factor = 1 / window.devicePixelRatio;
            var drawImage = CanvasRenderingContext2D.prototype.drawImage;
            CanvasRenderingContext2D.prototype.drawImage = function (image, sx, sy, sw, sh, dx, dy, dw, dh) {
                if (("nodeName" in image) && /img/i.test(image.nodeName)) {
                    for (var i = arguments.length - 1; i >= 1; i--) {
                        arguments[i] = arguments[i] * factor;
                    }
                } else if (typeof dw == "undefined") {
                    arguments[1] *= factor;
                    arguments[2] *= factor;
                    arguments[3] *= factor;
                    arguments[4] *= factor;
                }

                drawImage.apply(this, arguments);
            };
        }

		/**
		 * Check whether the user's browser supports Data URI or not
		 * 
		 * @private
		 * @param {Function} fSuccess Occurs if it supports Data URI
		 * @param {Function} fFail Occurs if it doesn't support Data URI
		 */
        function _safeSetDataURI(fSuccess, fFail) {
            var self = this;
            self._fFail = fFail;
            self._fSuccess = fSuccess;

            // Check it just once
            if (self._bSupportDataURI === null) {
                var el = document.createElement("img");
                var fOnError = function () {
                    self._bSupportDataURI = false;

                    if (self._fFail) {
                        self._fFail.call(self);
                    }
                };
                var fOnSuccess = function () {
                    self._bSupportDataURI = true;

                    if (self._fSuccess) {
                        self._fSuccess.call(self);
                    }
                };

                el.onabort = fOnError;
                el.onerror = fOnError;
                el.onload = fOnSuccess;
                el.src = "data:image/gif;base64,iVBORw0KGgoAAAANSUhEUgAAAAUAAAAFCAYAAACNbyblAAAAHElEQVQI12P4//8/w38GIAXDIBKE0DHxgljNBAAO9TXL0Y4OHwAAAABJRU5ErkJggg=="; // the Image contains 1px data.
                return;
            } else if (self._bSupportDataURI === true && self._fSuccess) {
                self._fSuccess.call(self);
            } else if (self._bSupportDataURI === false && self._fFail) {
                self._fFail.call(self);
            }
        };

		/**
		 * Drawing QRCode by using canvas
		 * 
		 * @constructor
		 * @param {HTMLElement} el
		 * @param {Object} htOption QRCode Options 
		 */
        var Drawing = function (el, htOption) {
            this._bIsPainted = false;
            this._android = _getAndroid();

            this._htOption = htOption;
            this._elCanvas = document.createElement("canvas");
            this._elCanvas.width = htOption.width;
            this._elCanvas.height = htOption.height;
            el.appendChild(this._elCanvas);
            this._el = el;
            this._oContext = this._elCanvas.getContext("2d");
            this._bIsPainted = false;
            this._elImage = document.createElement("img");
            this._elImage.alt = "Scan me!";
            this._elImage.style.display = "none";
            this._el.appendChild(this._elImage);
            this._bSupportDataURI = null;
        };

		/**
		 * Draw the QRCode
		 * 
		 * @param {QRCode} oQRCode 
		 */
        Drawing.prototype.draw = function (oQRCode) {
            var _elImage = this._elImage;
            var _oContext = this._oContext;
            var _htOption = this._htOption;

            var nCount = oQRCode.getModuleCount();
            var nWidth = _htOption.width / nCount;
            var nHeight = _htOption.height / nCount;
            var nRoundedWidth = Math.round(nWidth);
            var nRoundedHeight = Math.round(nHeight);

            _elImage.style.display = "none";
            this.clear();

            for (var row = 0; row < nCount; row++) {
                for (var col = 0; col < nCount; col++) {
                    var bIsDark = oQRCode.isDark(row, col);
                    var nLeft = col * nWidth;
                    var nTop = row * nHeight;
                    _oContext.strokeStyle = bIsDark ? _htOption.colorDark : _htOption.colorLight;
                    _oContext.lineWidth = 1;
                    _oContext.fillStyle = bIsDark ? _htOption.colorDark : _htOption.colorLight;
                    _oContext.fillRect(nLeft, nTop, nWidth, nHeight);

                    // 안티 앨리어싱 방지 처리
                    _oContext.strokeRect(
                        Math.floor(nLeft) + 0.5,
                        Math.floor(nTop) + 0.5,
                        nRoundedWidth,
                        nRoundedHeight
                    );

                    _oContext.strokeRect(
                        Math.ceil(nLeft) - 0.5,
                        Math.ceil(nTop) - 0.5,
                        nRoundedWidth,
                        nRoundedHeight
                    );
                }
            }

            this._bIsPainted = true;
        };

		/**
		 * Make the image from Canvas if the browser supports Data URI.
		 */
        Drawing.prototype.makeImage = function () {
            if (this._bIsPainted) {
                _safeSetDataURI.call(this, _onMakeImage);
            }
        };

		/**
		 * Return whether the QRCode is painted or not
		 * 
		 * @return {Boolean}
		 */
        Drawing.prototype.isPainted = function () {
            return this._bIsPainted;
        };

		/**
		 * Clear the QRCode
		 */
        Drawing.prototype.clear = function () {
            this._oContext.clearRect(0, 0, this._elCanvas.width, this._elCanvas.height);
            this._bIsPainted = false;
        };

		/**
		 * @private
		 * @param {Number} nNumber
		 */
        Drawing.prototype.round = function (nNumber) {
            if (!nNumber) {
                return nNumber;
            }

            return Math.floor(nNumber * 1000) / 1000;
        };

        return Drawing;
    })();

	/**
	 * Get the type by string length
	 * 
	 * @private
	 * @param {String} sText
	 * @param {Number} nCorrectLevel
	 * @return {Number} type
	 */
    function _getTypeNumber(sText, nCorrectLevel) {
        var nType = 1;
        var length = _getUTF8Length(sText);

        for (var i = 0, len = QRCodeLimitLength.length; i <= len; i++) {
            var nLimit = 0;

            switch (nCorrectLevel) {
                case QRErrorCorrectLevel.L:
                    nLimit = QRCodeLimitLength[i][0];
                    break;
                case QRErrorCorrectLevel.M:
                    nLimit = QRCodeLimitLength[i][1];
                    break;
                case QRErrorCorrectLevel.Q:
                    nLimit = QRCodeLimitLength[i][2];
                    break;
                case QRErrorCorrectLevel.H:
                    nLimit = QRCodeLimitLength[i][3];
                    break;
            }

            if (length <= nLimit) {
                break;
            } else {
                nType++;
            }
        }

        if (nType > QRCodeLimitLength.length) {
            throw new Error("Too long data");
        }

        return nType;
    }

    function _getUTF8Length(sText) {
        var replacedText = encodeURI(sText).toString().replace(/\%[0-9a-fA-F]{2}/g, 'a');
        return replacedText.length + (replacedText.length != sText ? 3 : 0);
    }

	/**
	 * @class QRCode
	 * @constructor
	 * @example 
	 * new QRCode(document.getElementById("test"), "http://jindo.dev.naver.com/collie");
	 *
	 * @example
	 * var oQRCode = new QRCode("test", {
	 *    text : "http://naver.com",
	 *    width : 128,
	 *    height : 128
	 * });
	 * 
	 * oQRCode.clear(); // Clear the QRCode.
	 * oQRCode.makeCode("http://map.naver.com"); // Re-create the QRCode.
	 *
	 * @param {HTMLElement|String} el target element or 'id' attribute of element.
	 * @param {Object|String} vOption
	 * @param {String} vOption.text QRCode link data
	 * @param {Number} [vOption.width=256]
	 * @param {Number} [vOption.height=256]
	 * @param {String} [vOption.colorDark="#000000"]
	 * @param {String} [vOption.colorLight="#ffffff"]
	 * @param {QRCode.CorrectLevel} [vOption.correctLevel=QRCode.CorrectLevel.H] [L|M|Q|H] 
	 */
    QRCode = function (el, vOption) {
        this._htOption = {
            width: 256,
            height: 256,
            typeNumber: 4,
            colorDark: "#000000",
            colorLight: "#ffffff",
            correctLevel: QRErrorCorrectLevel.H
        };

        if (typeof vOption === 'string') {
            vOption = {
                text: vOption
            };
        }

        // Overwrites options
        if (vOption) {
            for (var i in vOption) {
                this._htOption[i] = vOption[i];
            }
        }

        if (typeof el == "string") {
            el = document.getElementById(el);
        }

        if (this._htOption.useSVG) {
            Drawing = svgDrawer;
        }

        this._android = _getAndroid();
        this._el = el;
        this._oQRCode = null;
        this._oDrawing = new Drawing(this._el, this._htOption);

        if (this._htOption.text) {
            this.makeCode(this._htOption.text);
        }
    };

	/**
	 * Make the QRCode
	 * 
	 * @param {String} sText link data
	 */
    QRCode.prototype.makeCode = function (sText) {
        this._oQRCode = new QRCodeModel(_getTypeNumber(sText, this._htOption.correctLevel), this._htOption.correctLevel);
        this._oQRCode.addData(sText);
        this._oQRCode.make();
        this._el.title = sText;
        this._oDrawing.draw(this._oQRCode);
        this.makeImage();
    };

	/**
	 * Make the Image from Canvas element
	 * - It occurs automatically
	 * - Android below 3 doesn't support Data-URI spec.
	 * 
	 * @private
	 */
    QRCode.prototype.makeImage = function () {
        if (typeof this._oDrawing.makeImage == "function" && (!this._android || this._android >= 3)) {
            this._oDrawing.makeImage();
        }
    };

	/**
	 * Clear the QRCode
	 */
    QRCode.prototype.clear = function () {
        this._oDrawing.clear();
    };

	/**
	 * @name QRCode.CorrectLevel
	 */
    QRCode.CorrectLevel = QRErrorCorrectLevel;
})();/*
 * 版 本 Learun-Mobile V2.0.0 力软敏捷开发框架(http://www.learun.cn)
 * Copyright (c) 2013-2018 上海力软信息技术有限公司
 * 创建人：力软-前端开发组
 * 日 期：2017.12.15
 * 描 述：力软移动端框架 条码扫描插件
 */
(function ($, learun, fui, window) {
    "use strict";

    learun.code = {
        scan: function (callback) {
            if (learun.isreal) {
                cordova.plugins.barcodeScanner.scan(
                    function (result) {
                        if (result.text !== undefined && result.text !== null && result.text !== '') {
                            !!callback && callback({
                                status: 'success',
                                msg: result.text
                            });
                        }
                    },
                    function (error) {
                        !!callback && callback({
                            status: 'error',
                            msg: error
                        });
                    },
                    {
                        preferFrontCamera: false, // iOS and Android
                        showFlipCameraButton: false, // iOS and Android
                        showTorchButton: true, // iOS and Android
                        torchOn: false, // Android, launch with the torch switched on (if available)
                        saveHistory: true, // Android, save scan history (default false)
                        prompt: "将二维码/条码放入框内，即可自动扫描", // Android
                        resultDisplayDuration: 500, // Android, display scanned text for X ms. 0 suppresses it entirely, default 1500
                        formats: "QR_CODE,DATA_MATRIX,UPC_A,UPC_E,EAN_8,EAN_13,CODE_39,CODE_93,CODE_128,CODABAR,ITF,RSS14,PDF_417,RSS_EXPANDED", // default: all but PDF_417 and RSS_EXPANDED
                        //orientation: "landscape", // Android only (portrait|landscape), default unset so it rotates with the device
                        disableAnimations: true, // iOS
                        disableSuccessBeep: false // iOS and Android
                    }
                );
            }
            else {
                learun.layer.warning('浏览器环境不支持扫描', null, '力软提示', '好的');
            }
        },
        encode: function (op) {// id:div的id，text：需要生成的文本；width：宽度；height：高度；colorDark，colorLight；
            var qrcode = new QRCode(document.getElementById(op.id), {
                text: op.text || "http://www.learun.cn/",
                width: op.width || 128,
                height: op.height || 128,
                colorDark: op.colorDark || "#000000",
                colorLight: op.colorLight || "#ffffff",
                correctLevel: QRCode.CorrectLevel.H
            });
        }
    };


})(window.jQuery, window.lrmui, window.fui, window);/*
 * 版 本 Learun-Mobile V2.0.0 力软敏捷开发框架(http://www.learun.cn)
 * Copyright (c) 2013-2018 上海力软信息技术有限公司
 * 创建人：力软-前端开发组
 * 日 期：2018.01.08
 * 描 述：力软移动端框架 图片选择插件（支持拍照和从相册中选择）
 */
(function ($, learun, fui, window) {
    "use strict";
    var _ft = null;

    // 选择图片从相册中
    learun.imagePick = function (callback, op) {
        if (learun.isreal) {
            var dfop = {
                maximumImagesCount: 9,
                width: 1920,
                height: 1440,
                quality: 100
            };
            $.extend(dfop, op || {});

            ImagePicker.getPictures(function (result) {
                !!callback && callback(result);
            }, function (err) {
            }, dfop); 
        }
        else {
            learun.layer.warning('浏览器环境不支持图片选择', null, '力软提示', '好的');
        }
    };
    // 拍照获取图片
    learun.camera = function (callback) {
        if (learun.isreal) {
            navigator.camera.getPicture(
                function (imageURI) {
                    callback(imageURI);
                },
                function () {

                }, {
                    quality: 50,                                            // 相片质量是50  
                    sourceType: Camera.PictureSourceType.Camera,            // 设置摄像头拍照获取  
                    destinationType: Camera.DestinationType.FILE_URI,        // 以文件路径返回  
                    saveToPhotoAlbum: true
                });
        }
        else {
            learun.layer.warning('浏览器环境不支持拍照', null, '力软提示', '好的');
        }
    };

    var _imagepicker = {
        addImage: function (url, op, _fileId) {
            var $imagePicker = $('#' + op.id);
            var $imageHandle = $imagePicker.find('.lr-imagepicker-handle').parent();
            var _html = '\
                <div class="lr-imagepicker-item" >\
                    <img src="'+ url + '" />\
                    <div class="lr-imagepicker-remove" data-value="'+ _fileId + '" ><i class="iconfont icon-roundclosefill"></i><div></div></div>\
                </div>';
            $imageHandle.before(_html);
            $imageHandle = null;
        },
        upload: function (url, op, callback) {// 上传图片文件
            if (_ft === null) {
                _ft = new FileTransfer();
            }
            if (op.uploadUrl) {
                if (op.getParams) {
                    op.params = op.getParams();
                }
                op.params = op.params || {};
                op.params.data = op.value;
                _ft.upload(url, encodeURI(op.uploadUrl),
                    function (r) {
                        var _res = JSON.parse(r.response);
                        if (_res.code === 200) {
                            callback(true, url, op, _res.data);
                        }
                        else {
                            callback(false, url, op);
                        }
                    },
                    function (error) {
                        callback(false, url, op);
                    },
                    {
                        chunkedMode: false,
                        params: op.params || {}
                    }
                );
            }
            else {
                callback(true, url, op, learun.guid());
            }
        },
        uploads: function (data, index, op, callback, callback2) {
            if (data.length > index) {
                _imagepicker.upload(data[index], op, function (isOk, _url, _op, _fileId) {
                    callback(isOk, _url, _op, _fileId);
                    _imagepicker.uploads(data, index + 1, op, callback, callback2);
                });
            }
            else {
                callback2();
            }
        },
        down: function (imgUrl) {
            cordova.plugins.photoLibrary.requestAuthorization(
                function () {
                    // User gave us permission to his library, retry reading it! 
                    cordova.plugins.photoLibrary.getLibrary(
                        function ({ library }) {
                            //var url = 'file:///...'; // file or remote URL. url can also be dataURL, but giving it a file path is much faster 
                            var album = 'learunADMSApp';
                            cordova.plugins.photoLibrary.saveImage(imgUrl, album,
                                function (libraryItem) {
                                    learun.layer.toast('保存成功');
                                }, function (err) {
                                    learun.layer.toast('保存失败' + err);
                                });
                        },
                        function (err) {
                            if (err.startsWith('Permission')) {
                                // call requestAuthorization, and retry 
                            }
                            // Handle error - it's not permission-related 
                            console.log('权限' + err);

                        }
                    );
                },
                function (err) {
                    learun.layer.toast('用户拒绝访问' + + err);
                    // User denied the access 
                }, // if options not provided, defaults to {read: true}. 
                {
                    read: true,
                    write: true
                }
            );
        }
    };

    $.fn.imagepicker = function (op) {
        var dfop = {
            maxCount: 9,
            isOnlyCamera: false,
            params: {}
        };
        $.extend(dfop, op || {});
        var $this = $(this);
        $this[0].op = dfop;
        
        var id = $this.attr('id');
        if (!id) {
            id = learun.guid();
            $this.attr('id', id);
        }
        dfop.id = id;
        dfop.value = learun.guid();

       

        $this.addClass('lr-imagepicker');
        $this.attr('type','lrimagepicker');
        $this.html('\
            <div class="lr-imagepicker-item">\
                <div class="lr-imagepicker-handle" ><i class="iconfont icon-add1"></i></div >\
            </div>');
        $this.find('.lr-imagepicker-handle').on('tap', function () {
            var $this = $(this);
            if ($this.attr('readonly') || $this.parents('.lr-form-row').attr('readonly')) {
                return false;
            }

            var $imagePicker = $this.parents('.lr-imagepicker');
            var op = $imagePicker[0].op;

            if (dfop.isOnlyCamera) {
                learun.camera(function (res) {
                    learun.layer.loading(true, '正在上传...');
                    // 上传文件
                    _imagepicker.upload(res, op, function (isOk, _url, _op, _fileId) {
                        learun.layer.loading(false);
                        if (isOk) {
                            _imagepicker.addImage(_url, _op, _fileId);
                        }
                    });
                });
            } else {
                var _data = [{
                    text: '拍照',
                    event: function () {
                        learun.camera(function (res) {
                            learun.layer.loading(true, '正在上传...');
                            // 上传文件
                            _imagepicker.upload(res, op, function (isOk, _url, _op, _fileId) {
                                learun.layer.loading(false);
                                if (isOk) {
                                    _imagepicker.addImage(_url, _op, _fileId);
                                }
                            });
                        });
                    }
                }, {
                    text: '从手机相册选择',
                    event: function () {
                        learun.imagePick(function (res) {
                            learun.layer.loading(true, '正在上传...');
                            _imagepicker.uploads(res, 0, op, function (isOk, _url, _op, _fileId) {
                                if (isOk) {
                                    _imagepicker.addImage(_url, _op, _fileId);
                                }
                            }, function () {
                                learun.layer.loading(false);
                            });
                        });
                    }
                }];

                learun.actionsheet({
                    id: 'lrimagepicker',
                    data: _data
                });
            }

        });

        $this.delegate('.lr-imagepicker-remove>i', 'tap', { op: dfop }, function (e) {
            var op = e.data.op;
            var fileId = $(this).parent().attr('data-value');
            var $imapge = $(this).parent().parent();
            $imapge.remove();
            $imapge = null;

            op.deleteImg && op.deleteImg(fileId);
            return false;
        });

        $this.delegate('img', 'tap', { op: dfop }, function (e) {
            var op = e.data.op;

            var data = [];
            var fileId = $(this).parent().find('.lr-imagepicker-remove').attr('data-value');
            var _index = 0;
            $(this).parent().parent().find('img').each(function () {
                var _fileId = $(this).parent().find('.lr-imagepicker-remove').attr('data-value');
                var src = $(this).attr('src');
                if (_fileId === fileId) {
                    _index = data.length;
                }
                data.push(src);
            });
            fui.imagePreview({
                data: data, gotonum: _index,
                holdEvent: function (_src) {
                    var _data = [{
                        text: '保存图片',
                        event: function () {
                            if (_src.indexOf('http://') !== -1) {
                                _imagepicker.down(_src, op);
                            }
                            else {
                                learun.layer.toast('本地图片无需保存');
                            }
                        }
                    }];

                    learun.actionsheet({
                        id: 'lrimagepicker',
                        data: _data
                    });
                }
            });
            return false;
        });

        return $this;
    };

    $.fn.imagepickerGet = function () {
        var $this = $(this);
        if ($this.hasClass('lr-imagepicker')) {
            var _op = $this[0].op;
            $this = null;
            return _op.value;
        }
        $this = null;
        return '';
    }

    $.fn.imagepickerSet = function (value) {
        if (value != undefined && value != null && value != '' && value != 'undefined' && value != 'null') {
            var $this = $(this);
            if ($this.hasClass('lr-imagepicker')) {
                var op = $this[0].op;
                op.value = value;
                op.downFile(value, function (data) {
                    $.each(data, function (index, _item) {
                        _imagepicker.addImage(op.downUrl + _item.name, op, _item.id);
                    });
                });
            }
        }
    }

})(window.jQuery, window.lrmui, window.fui, window);/*
 * 版 本 Learun-Mobile V2.0.0 力软敏捷开发框架(http://www.learun.cn)
 * Copyright (c) 2013-2018 上海力软信息技术有限公司
 * 创建人：力软-前端开发组
 * 日 期：2017.12.15
 * 描 述：力软移动端框架 进度条插件
 */
(function ($, learun, fui, window) {
    "use strict";
    $.fn.progressSet = function (value) {
        if (value === undefined || value === null || value === "") {
            return;
        }
        var _style = 'transform: translate3d('+(value - 100)+'%, 0px, 0px);';
        $(this).find('span').attr('style', _style);
    };

})(window.jQuery, window.lrmui, window.fui, window);/*
 * 版 本 Learun-Mobile V2.0.0 力软敏捷开发框架(http://www.learun.cn)
 * Copyright (c) 2013-2018 上海力软信息技术有限公司
 * 创建人：力软-前端开发组
 * 日 期：2018.07.06
 * 描 述：日期查询选择框
 */
(function ($, learun, fui, window) {
    "use strict";

    var _multiplequery = {
        init: function ($self, op) {
            var $content = $self.find('.lr-tool-right-btn-content').show();
            $content.parents('.f-page').append($content);
           
            $content.show();
            $content.fpopright({
                callBack: function (type, $content) {
                    if (type === 'ok') {// 确定
                        setTimeout(function () {
                            var data = $content.lrformGet();
                            op.callback && op.callback(data);
                        }, 300);
                    }
                    else if (type === 'rest') {// 重置
                        setTimeout(function () {
                            var data = $content.lrformGet();
                            $.each(data, function (_id, _item) {
                                data[_id] = "";
                            });
                            $content.lrformSet(data);
                        }, 300);
                    }
                }
            });
            $content.parents('.f-popright-body').addClass('lr-tool-right-btn-body ');
            $self.on('tap', { $content: $content }, function (e) {
                e.data.$content.fpoprightShow();
            });

            return $content;
        }
    };

    $.fn.multiplequery = function (op) {
        var $this = $(this);
        if ($this.length === 0) {
            return $this;
        }
        if ($this[0].dfop) {
            return $this;
        }
        var dfop = {
            callback: false
        };
        $.extend(dfop, op || {});
        $this[0].dfop = dfop;
        return _multiplequery.init($this, dfop);
    };

})(window.jQuery, window.lrmui, window.fui, window);/*
 * 版 本 Learun-Mobile V2.0.0 力软敏捷开发框架(http://www.learun.cn)
 * Copyright (c) 2013-2018 上海力软信息技术有限公司
 * 创建人：力软-前端开发组
 * 日 期：2018.07.06
 * 描 述：日期查询选择框
 */
(function ($, learun, fui, window) {
    "use strict";

    var _searchdate = {
        init: function ($self, op) {
            var _html = '\
            <div class="lr-search-date" >\
                <a href="javascript:;" class="lr-search-date-btn" data-value="0">今天</a>\
                <a href="javascript:;" class="lr-search-date-btn" data-value="1">近7天</a>\
                <a href="javascript:;" class="lr-search-date-btn" data-value="2">近1个月</a>\
                <a href="javascript:;" class="lr-search-date-btn" data-value="3">近3个月</a>\
                <a href="javascript:;" class="lr-search-date-btn active" data-value="4">自定义</a>\
                <div class="lr-search-date-custmer">\
                    <div class="lr-form-row"><label>开始时间</label><div id="lr_search_date_custmer1"></div></div>\
                    <div class="lr-form-row"><label>结束时间</label><div id="lr_search_date_custmer2"></div></div>\
                </div >\
            </div>';

            var _$html = $(_html);
            $self.parents('.f-page').append(_$html);


         

            _$html.fpopright({
                restBtn: '',
                callBack: function (type, $content) {
                    if (type === 'ok') {
                        var btn = $content.find('.lr-search-date-btn.active').attr('data-value');
                        if (btn === '4') {
                            setTimeout(function () {
                                var begin = ($content.find('#lr_search_date_custmer1').lrdateGet() || '1000-01-01') + " 00:00:00";
                                var end = ($content.find('#lr_search_date_custmer2').lrdateGet() || fui.date.get('yyyy-MM-dd')) + " 23:59:59";
                                op.callback && op.callback(begin, end);
                            }, 300);
                        }
                    }
                }
            });

            _$html.find('#lr_search_date_custmer1').lrdate({
                type: 'date'
            });
            _$html.find('#lr_search_date_custmer2').lrdate({
                type: 'date'
            });
            _$html.find('.lr-search-date-btn').on('tap', { dfop: op }, function (e) {
                var $this = $(this);
                var v = $this.attr('data-value');
                var _op = e.data.dfop;
                var $p = $this.parents('.lr-search-date');
                var begin = '';
                var end = '';
                $p.find('.lr-search-date-custmer').hide();
                $p.find('.lr-search-date-btn').removeClass('active');
                $this.addClass('active');

                switch (v) {
                    case '0':// 今天
                        $p.fpoprightHide();
                        setTimeout(function () {
                            begin = fui.date.get('yyyy-MM-dd 00:00:00');
                            end = fui.date.get('yyyy-MM-dd 23:59:59');
                            _op.callback && _op.callback(begin, end);
                        }, 300);
                        break;
                    case '1':// 近7天
                        $p.fpoprightHide();
                        setTimeout(function () {
                            begin = fui.date.get('yyyy-MM-dd 00:00:00', 'd', -6);
                            end = fui.date.get('yyyy-MM-dd 23:59:59');
                            _op.callback && _op.callback(begin, end);
                        }, 300);
                        break;
                    case '2':// 近1个月
                        $p.fpoprightHide();
                        setTimeout(function () {
                            begin = fui.date.get('yyyy-MM-dd 00:00:00', 'm', -1);
                            end = fui.date.get('yyyy-MM-dd 23:59:59');
                            _op.callback && _op.callback(begin, end);
                        }, 300);
                        break;
                    case '3':// 近3个月
                        $p.fpoprightHide();
                        setTimeout(function () {
                            begin = fui.date.get('yyyy-MM-dd 00:00:00', 'm', -3);
                            end = fui.date.get('yyyy-MM-dd 23:59:59');
                            _op.callback && _op.callback(begin, end);
                        }, 300);
                        break;
                    case '4':// 自定义
                        $p.find('.lr-search-date-custmer').show();
                        break;
                }
            });

            $self.on('tap', { $content: _$html }, function (e) {
                e.data.$content.fpoprightShow();
            });
        }
    };

    $.fn.searchdate = function (op) {
        var $this = $(this);
        if ($this.length === 0) {
            return $this;
        }
        if ($this[0].dfop) {
            return $this;
        }
        var dfop = {
            callback: false
        };
        $.extend(dfop, op || {});
        $this[0].dfop = dfop;
        _searchdate.init($this, dfop);
        return $this;
    }; 

})(window.jQuery, window.lrmui, window.fui, window);/*
 * 版 本 Learun-Mobile V2.0.0 力软敏捷开发框架(http://www.learun.cn)
 * Copyright (c) 2013-2018 上海力软信息技术有限公司
 * 创建人：力软-前端开发组
 * 日 期：2018.06.12
 * 描 述：力软移动端框架 列表左移按钮
 */
(function ($, learun, fui, window) {
    "use strict";

    var _lrlistswipe = {
        init: function ($this, op) {
            $this.addClass('f-swipebtn');
            var $handler = $('<div class="f-swipebtn-handler">' + $this.html() + '</div>');
            var $btn = $('<div class="f-swipebtn-right"></div>');
            $this.html($handler);
            $this.append($btn);
            $.each(op.btns, function (_index, _item) {
                var _$item = $(_item).addClass('f-swipebtn-btn');
                $btn.append(_$item);
            });

            $this.fswipebtn();

            $this = null;
            $handler = null;
            $btn = null;
        }
    };

    $.fn.lrlistswipe = function (op) {
        var dfop = {
            btns: []
        };
        $.extend(dfop, op || {});
        $(this).each(function () {
            _lrlistswipe.init($(this), dfop);
        });

    };
})(window.jQuery, window.lrmui, window.fui, window);/*
 * 版 本 Learun-Mobile V2.0.0 力软敏捷开发框架(http://www.learun.cn)
 * Copyright (c) 2013-2018 上海力软信息技术有限公司
 * 创建人：力软-前端开发组
 * 日 期：2017.12.15
 * 描 述：力软移动端框架 多选框插件
 */
(function ($, learun, fui, window) {
    "use strict";

    // 多选框(初始化)
    $.fn.lrcheckbox = function (op) {
        var $this = $(this);
        if ($this.length === 0) {
            return $this;
        }
        var dfop = {
            placeholder: '请选择',
            data: [],
            ivalue: 'value',
            itext: 'text',
            change: false
        };
        $.extend(dfop, op || {});
        dfop.callback = function () {
            learun.formblur();
        };
        $this.attr('type', 'lrcheckbox');
        $this.addClass('lr-checkbox');
        $this.html('<div class="text">' + dfop.placeholder + '</div>');

        setTimeout(function () {
            $this.fcheckbox(dfop).on('change', function () {
                var $self = $(this);
                var text = $self[0].fop.text || '';
                var $text = $self.find('.text');
                if (text === ''){
                    $self.find('.text').text($self[0].fop.placeholder);
                }
                else {
                    $text.html('');
                    var textlist = text.split(',');
                    $.each(textlist, function (_index, _item) {
                        var _html = '<div class="lr-checkbox-item" >' + _item + '</div>';
                        $text.append(_html);
                    });
                }
                $text = null;
            });
        }, 100);

        return $this;
    };
    // 多选框(获取数据值)
    $.fn.lrcheckboxGet = function (type) {
        var $this = $(this);
        if ($this.length === 0) {
            return "";
        }
        var fop = $this[0].fop;
        if (type === 'text') {
            return fop.text;
        }
        else {
            return fop.value;
        }
    };
    // 多选框(设置数据值)
    $.fn.lrcheckboxSet = function (value) {
        var $this = $(this);
        if ($this.length === 0) {
            return false;
        }
        function set(value, $this) {
            if (!$this[0].fop) {
                setTimeout(function () {
                    set(value, $this);
                }, 100);
            }
            else {
                var fop = $this[0].fop;
                if (value) {
                    fop._lrTmpValue = value;
                }
                $this.fcheckboxSet(value);
                if (value === '') {
                    fop.value = '';
                    fop.text = '';
                    $this.find('.text').text(fop.placeholder);
                }
            }
        }
        set(value, $this);
    };
    // 多选框(更新数据)
    $.fn.lrcheckboxSetData = function (data) {
        var $this = $(this);
        if ($this.length === 0) {
            return false;
        }
        function updateData(data, $this) {
            if (!$this[0].fop) {
                setTimeout(function () {
                    updateData(data, $this);
                }, 100);
            }
            else {
                $this.fcheckboxSetData(data);
                $this.lrcheckboxSet($this[0].fop._lrTmpValue);
            }
        }
        updateData(data, $this);
    };

})(window.jQuery, window.lrmui, window.fui, window);