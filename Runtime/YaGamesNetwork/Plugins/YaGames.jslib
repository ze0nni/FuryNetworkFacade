mergeInto(LibraryManager.library, {
	YaGamesInit: function() {
		var scriptSrc = "https://yandex.ru/games/sdk/v2";
		var script = document.createElement("script");
		script.src = scriptSrc;
		script.onload = () => {
			console.log("Loaded: " + scriptSrc);
			initSDK();
		};
		script.onerror = () => {
			var error = "Error load script from: " + scriptSrc;
			SendMessage()
			console.error(error);
			SendMessage("Fury.NetworkFacade", "InitCallbackError", error);
		}'

		console.log("Start load: " + scriptSrc);
		document.body.appendChild(script);

		function initSDK() {
			console.log("YaGames.init: ...");
			YaGames
				.init()
				.then(sdk => {
					window.ySDK = sdk;
					console.log("YaGames.init: ok");
					if (sdk.features.LoadingAPI) {
						sdk.features.LoadingAPI.ready()
					}
					SendMessage("Fury.NetworkFacade", "InitCallbackOk");
				})
				.catch(error => {
					console.error("YaGames.init: ", error);
					SendMessage("Fury.NetworkFacade", "InitCallbackError", error.toString());
				});
		}
	}
});