// export class LocalStorage {
//     length = localstorage.length
//    
//     key(index) {
//         return localStorage.key(index)
//     }
//
//     getItem(key) {
//         return localStorage.getItem(key)
//     }
//
//     setItem(key, value) {
//         return localStorage.setItem(key, value)
//     }
//
//     removeItem(key) {
//         return localStorage.removeItem(key)
//     }
//
//     clear() {
//         return localStorage.clear()
//     }
// }

export function getItem(key) {
    return localStorage.getItem(key)
}