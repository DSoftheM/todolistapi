import { useQuery } from "react-query";
import { httpClient } from "../axios";

export function AllTasksList() {
  const allQuery = useQuery({
    queryKey: ["all-tasks"],
    queryFn: () => httpClient.get("/task/getAll"),
  });

  return (
    <>
      <h2>Список задач</h2>
      <pre>{JSON.stringify(allQuery.data?.data ?? {}, null, 2)}</pre>
    </>
  );
}
