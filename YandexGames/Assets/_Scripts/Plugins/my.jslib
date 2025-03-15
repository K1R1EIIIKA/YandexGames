mergeInto(LibraryManager.library, {
    GetPlayerData: function() {
        if (typeof myGameInstance !== "undefined" && myGameInstance !== null) {
            myGameInstance.SendMessage("JsLib", "SetPlayerName", player.getName());
            myGameInstance.SendMessage("JsLib", "SetPlayerImage", player.getPhoto('medium'));
        } else {
            console.error("myGameInstance is not defined or null!");
        }
    },
    GetYandexLanguage: function() {
        if (typeof ysdk !== 'undefined' && ysdk.environment && ysdk.environment.i18n) {
            var lang = ysdk.environment.i18n.lang;
            var bufferSize = lengthBytesUTF8(lang) + 1;
            var buffer = _malloc(bufferSize);
            stringToUTF8(lang, buffer, bufferSize);
            console.log("Yandex language: " + lang);
            return buffer;
        } else {
            console.error("Yandex SDK или необходимые свойства не определены.");
            return 0;
        }
    },
});
