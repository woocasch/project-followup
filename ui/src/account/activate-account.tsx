import { PageHeader } from "@root/components";
import { useParams } from "react-router";

type RouteParams = Record<'linkCode', string>;

export default function ActivateAccount() {
    const { linkCode } = useParams<RouteParams>();
    
    return (<div>
        <PageHeader>Activate Account</PageHeader>
        <p>Hello <span>{linkCode}</span></p>
        <p>To activate your account please click <a href="#">here</a>.</p>
    </div>)
}