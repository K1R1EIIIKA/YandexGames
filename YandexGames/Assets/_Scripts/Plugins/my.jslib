mergeInto(LibraryManager.library, {
    GetPlayerData: function() {
        if (typeof myGameInstance !== "undefined" && myGameInstance !== null) {
            myGameInstance.SendMessage("JsLib", "SetPlayerName", player.getName());
            myGameInstance.SendMessage("JsLib", "SetPlayerImage", player.getPhoto('medium'));
        } else {
            console.error("myGameInstance is not defined or null!");
        }
    },
});
