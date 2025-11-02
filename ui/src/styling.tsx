import { Card, PageHeader, CardSize } from "./components";

export default function StylingPage() {
    return <div>
        <PageHeader>Styling guidelines</PageHeader>
        <p>This page will contain styling guidelines for the project.</p>
        <div style={{ width: "400px" }}>
            <h3>Cards</h3>
            <Card size={CardSize.Small}>
                <Card.Header>
                    <h2>Card Title</h2>
                </Card.Header>
                <Card.Content>
                    <p>This is the card content</p>
                </Card.Content>
                <Card.Footer>
                    <button>Action</button>
                </Card.Footer>
            </Card>
        </div>
    </div>
}