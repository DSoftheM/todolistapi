import { useMutation } from "react-query"
import { httpClient } from "../../axios"

type RegisterData = {
    login: string
    password: string
}

export function useRegister() {
    return useMutation({
        mutationFn: (data: RegisterData) => httpClient.post("/auth/register", data),
    })
}
