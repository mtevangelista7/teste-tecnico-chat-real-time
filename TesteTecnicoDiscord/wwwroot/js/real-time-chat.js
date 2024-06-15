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

function scrollToBottom(elementId) {
    const element = document.getElementById(elementId);
    if (element) {
        element.scrollTop = element.scrollHeight;
    }
}