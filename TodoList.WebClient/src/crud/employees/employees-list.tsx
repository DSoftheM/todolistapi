import { Typography, Flex } from "antd";
import { useEmployeesList } from "./use-employees-list";
import { EmployeeListItem } from "./employee-list-item";

export function EmployeesList() {
  const employeesListQuery = useEmployeesList();

  return (
    <div>
      <Typography.Title>Работники</Typography.Title>
      <Flex style={{ display: "inline-flex" }} vertical>
        {!employeesListQuery.data?.length && (
          <Typography.Text>Нет работников</Typography.Text>
        )}
        {employeesListQuery.data?.map((x) => {
          return <EmployeeListItem key={x.id} employee={x} />;
        })}
      </Flex>
    </div>
  );
}
