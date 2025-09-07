import { columns, type Project } from "./ProjectTableColumns"
import { DataTable } from "./DataTable"
import { apiService } from "../lib/Request";
import { getCookie, usernameCookieName } from "./Authentication";
import { useEffect, useState } from "react";

async function getData() {
    var username = getCookie(usernameCookieName)
    const [projects, setProjects] = useState<Project[]>([]);

    useEffect(() => {
        // This is where I got stuck 
        var projects = apiService.post<any, Project[]>("/projects", {
            username
        })
    });
}

export default function DemoPage() {
    const data = getData()

    return (
        <div className="container mx-auto py-10">
            <DataTable columns={columns} data={data} />
        </div>
    )
}