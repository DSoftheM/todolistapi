import axios from "axios";

console.log("import.meta.env :>> ", import.meta.env);

export const httpClient = axios.create({
  baseURL: import.meta.env.VITE_BASE_URL,
});
