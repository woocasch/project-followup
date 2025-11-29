import getConfig, { isConfigLoaded } from "@root/infrastructure/configuration";
import axios from "axios";

export abstract class ApiClientBase {
    protected createBffClient() {
        return axios.create({
            baseURL: getConfig().bffRoot,
            headers: {
                'Content-Type': 'application/json',
            },
        });
    }

    private getApiRoot(): string {
        if (!isConfigLoaded()) {
            return '';
        }

        return getConfig().bffRoot;
    }
}