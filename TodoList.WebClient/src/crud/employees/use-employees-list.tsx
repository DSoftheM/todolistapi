import { useQuery } from "react-query";
import { httpClient } from "../../axios";

export type Employee = {
	id: string;
	name: string;
};

export function useEmployeesList() {
	return useQuery<Employee[]>({
		queryKey: ["employees"],
		queryFn: async () => (await httpClient.get("/employee/getAll")).data,
	});
}
