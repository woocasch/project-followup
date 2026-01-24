import { PageHeader } from "@root/components";
import { useNavigate, useParams } from "react-router";
import accountActivationService from "./account-activation/account-activation.service";
import { useEffect, useState } from "react";

type RouteParams = Record<'linkCode', string>;

export default function ActivateAccount() {
    const { linkCode } = useParams<RouteParams>();
    const navigate = useNavigate();

    const [displayName, setDisplayName] = useState('');
    const [emailAddress, setEmailAddress] = useState('');


    useEffect(() => {
        if (!linkCode) {
            navigate('/');
            return;
        }

        accountActivationService.getLinkCodeData(linkCode)
        .then((r) => {
            if (r) {
                setDisplayName(r.displayName);
                setEmailAddress(r.emailAddress);
            }
        });
    }, [linkCode, navigate]);

    return (<div>
        <PageHeader>Account activation</PageHeader>
        <p>Hello <span>{displayName}</span></p>
        <p>You were invited to <strong>Project Follow-Up</strong> application through e-mail address <em>{emailAddress}</em></p>
        <p>To activate your account please click <a href="#">here</a>.</p>
    </div>)
}