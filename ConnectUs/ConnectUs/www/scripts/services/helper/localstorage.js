function saveItemToLocalStorage(key, value) {
    window.localStorage.setItem(key, value);
}

function getItemFromLocalStorage(key) {
    return window.localStorage.getItem(key);
}

function getBooleanFromLocalStorage(key) {
    return getItemFromLocalStorage(key) === 'true' ? true : false;
}

function removeItemFromLocalStorage(key) {
    return window.localStorage.removeItem(key);
}