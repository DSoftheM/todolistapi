import { Employee } from "../../employees/use-employees-list"

export type Assignment = {
    id: string
    title: string
    text: string
    created: Date
    employees: Employee[]
    done: boolean
    priority: AssignmentPriority
}

export enum AssignmentPriority {
    Low = "Low",
    Medium = "Medium",
    High = "High",
}

export function assignmentPriorityToString(assignmentPriority: AssignmentPriority) {
    switch (assignmentPriority) {
        case AssignmentPriority.Low:
            return "Низкий"
        case AssignmentPriority.Medium:
            return "Средний"
        case AssignmentPriority.High:
            return "Высокий"
    }
}
