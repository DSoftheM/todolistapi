import { CreateTask } from "./crud/assignments/create-task";
import { AllTasksList } from "./crud/assignments/all-tasks-list";
import { Flex } from "antd";
import { EmployeesList } from "./crud/employees/employees-list";
import { EmployeeCreation } from "./crud/employees/employee-creation";

function App() {
	return (
		<div>
			<Flex gap={40}>
				<CreateTask />
				<AllTasksList />
			</Flex>
			<hr />
			<EmployeesList />
			<EmployeeCreation />
		</div>
	);
}

export default App;
