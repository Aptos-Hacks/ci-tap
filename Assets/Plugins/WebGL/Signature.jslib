mergeInto(LibraryManager.library, {
  SendPayload: function (action, payloadMessage) {
    window.dispatchReactUnityEvent("SendPayload", UTF8ToString(action), UTF8ToString(payloadMessage));
  },
  Back: function () {
    window.dispatchReactUnityEvent("Back");
  }, 
});