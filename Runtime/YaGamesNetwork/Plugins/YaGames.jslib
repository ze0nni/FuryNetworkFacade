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
			console.error(error);
			SendMessage("Fury.NetworkFacade", "OnInitCallbackError", error);
		}

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
					SendMessage("Fury.NetworkFacade", "OnInitCallbackOk");
				})
				.catch(error => {
					console.error("YaGames.init: ", error);
					SendMessage("Fury.NetworkFacade", "OnInitCallbackError", error.toString());
				});
		}
	},

	YaGamesGetData: function() {
		var SDK = window.ySDK;
		SDK
			.getPlayer()
			.then(player => player.getData())
			.then(data => {				
				SendMessage("Fury.NetworkFacade", "OnGetDataCallbackOk", JSON.stringify(data));
			})
			.catch(error => {
				console.error("YaGames.getData: ", error);
				SendMessage("Fury.NetworkFacade", "OnGetDataCallbackError", error.toString());
			})
	},

	YaGamesSetData: function(jsonPtr) {
		var json = UTF8ToString(jsonPtr);
		var data = JSON.parse(json);
		var SDK = window.ySDK;

		var ver = (window.__ySDKDataV__ | 0) + 1;
		window.__ySDKDataV__ = ver;

		SDK
			.getPlayer()
			.then(player => {
				if (window.__ySDKDataV__ == ver) {
					return player.setData(data)
				} else {
					return true;
				}
			})
			.then(() => console.log("YaGames.SetData: v" + ver))
			.catch(error => {
				console.error("YaGames.SetData: " + error)
			})
	}
});