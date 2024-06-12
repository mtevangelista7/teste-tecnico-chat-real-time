window.addBeforeUnloadListener = function (dotNetRef) {
    window.addEventListener('beforeunload', function (event) {
        dotNetRef.invokeMethodAsync('HandleBeforeUnload');
    });
};

window.removeBeforeUnloadListener = function (dotNetRef) {
    window.removeEventListener('beforeunload', function (event) {
        dotNetRef.invokeMethodAsync('HandleBeforeUnload');
    });
};