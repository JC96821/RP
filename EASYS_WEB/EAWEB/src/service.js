import axios from 'axios'

const Api = {}
axios.defaults.baseURL = '/api'
axios.defaults.headers.post['Content-Type'] = 'application/json'

Api.get = function get(url, params = {}) {
  return new Promise((resolve, reject) => {
    axios.get(url, {params: params})
      .then(response => {
        resolve(response.data);
      }).catch(err => {
        reject(err)
      })
  })
}
Api.post = function post(url, params = {}) {
  return new Promise((resolve, reject) => {
    axios.post(url, params)
      .then(response => {
        resolve(response.data);
      }, err => {
        reject(err)
      })
  })
}

export default Api
