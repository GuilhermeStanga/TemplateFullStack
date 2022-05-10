import http from "../http-common";

class DataService {
  getAll(name) {
    return http.get(`/students?name=${name}`);
  }

  get(id: number) {
    return http.get(`/students/${id}`);
  }

  create(data) {
    return http.post("/students", data);
  }

  update(id, data) {
    return http.patch(`/students/${id}`, data);
  }

  delete(id) {
    return http.delete(`/students/${id}`);
  }

  findByName(name) {
    return http.get(`/students?name=${name}`);
  }
}

export default new DataService();