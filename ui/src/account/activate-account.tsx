import { Button, PageHeader, Text } from "@root/components";
import { useNavigate, useParams } from "react-router";
import accountActivationService from "./account-activation/account-activation.service";
import { useEffect, useState } from "react";
import styled from "@emotion/styled";
import { theme } from "@root/theme";

const DisplayContainer = styled.div(`
    margin: auto;
    width: 50%;
    display: grid;
    grid-template-columns: 1fr 1fr;
    &>*.description {
        grid-column: span 2;
    }
    gap: ${theme.spaces.large};
`);

const Header = styled.p(`
    font-size: ${theme.fontSizes.large};
    font-weight: bold;
    text-align: center;
`);

const UserDatum = styled.span(`
    font-style: italic;
`);

const Buttons = styled.div(`
    grid-column: span 2;
    text-align: center;
`);

type RouteParams = Record<'linkCode', string>;

export default function ActivateAccount() {
    const { linkCode } = useParams<RouteParams>();
    const navigate = useNavigate();

    const [displayName, setDisplayName] = useState('');
    const [emailAddress, setEmailAddress] = useState('');
    const [password, setPassword] = useState('');
    const [repeatPassword, setRepeatPassword] = useState('');

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

    return (<DisplayContainer>
        <PageHeader className="description">Account activation</PageHeader>
        <Header className="description">Hello <UserDatum>{displayName}</UserDatum></Header>
        <p className="description">You were invited to <strong>Project Follow-Up</strong> application through e-mail address <UserDatum>{emailAddress}</UserDatum></p>
        <p className="description">Select a password to activate your account:</p>
        <Text label="Password" hiddenValue={true} value={password} setValue={setPassword} />
        <Text label="Repeat password" hiddenValue={true} value={repeatPassword} setValue={setRepeatPassword} />
        <Buttons>
            <Button variant="button" buttonType="rounded" onClick={() => alert('test')}>Activate account</Button>
        </Buttons>
    </DisplayContainer>)
}