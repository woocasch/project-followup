import styled from "@emotion/styled";
import { theme } from '../theme';

const StyledFooter = styled.footer(`
    width: 100%;
    height: 3em;
    line-height: 3em;
    position: absolute;
    bottom: 0;
    text-align: center;
    background-color: ${theme.colors.panelheader};
`);

export default function Footer(){
    return <StyledFooter>
    Project Follow-Up app supported by <a href="https://lukasznowakowski.it">https://lukasznowakowski.it</a>.
    </StyledFooter>;
}
