import axios from "axios";

export function getApiErrorMessage(error: unknown): string {
  if (axios.isAxiosError(error)) {
    if (error.response?.data?.detail) {
      return error.response.data.detail;
    }

    if (error.response?.data?.title) {
      return error.response.data.title;
    }

    if (error.response?.status === 401) {
      return "You are not authorized.";
    }

    if (error.response?.status === 403) {
      return "You do not have permission to perform this action.";
    }

    if (error.response?.status === 404) {
      return "The requested resource was not found.";
    }

    if (!error.response) {
      return "Unable to connect to the server.";
    }
  }

  return "Something went wrong. Please try again.";
}
