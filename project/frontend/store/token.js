export const state = () => ({
  token: 'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9zaWQiOiIxIiwibmJmIjoxNjE5MzE2MjMzLCJleHAiOjE2MTk0MDI2MzMsImlzcyI6IlJhZG9seW4iLCJhdWQiOiJSYWRvbHluIn0.svi8YfYR4wX2j6vUPEC3vNTukKQE0TD-UmiqPuVaKhk'
})

export const mutations = {
  set(state, token) {
    state.token = token
  }
}
