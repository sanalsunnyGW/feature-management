import { FormControl } from "@angular/forms";
import { FeatureStatus, FeatureType } from "../enum/feature.enum";

export interface ILoginForm {
    email: FormControl<string | null>;
    password: FormControl<string | null>;
}

export interface ILoginAccept {
    email: string | null;
    password: string | null;
}

export interface ISignUpForm {
    fullName: FormControl<string | null>;
    email: FormControl<string | null>;
    password: FormControl<string | null>;
    confirmPassword: FormControl<string | null>;
}

export interface ISignUpAccept {
    name: string | null;
    email: string | null;
    password: string | null;
}

export interface IFeature {
    FeatureId: number;
    name: string;
    type: FeatureType;
    status: FeatureStatus;
}

export interface IRetrievedFeatures {
    featureFlagId: number;
    featureId: number;
    featureName: string;
    featureType: number;
    isEnabled: boolean | null;
}

export interface IPaginatedFeatures {
    pageSize: number;
    featureCount: number;
    totalPages: number;
    featureList: IRetrievedFeatures[];
}

export interface IselectedFilters {
    featureFilter: boolean | null;
    releaseFilter: boolean | null;
    enabledFilter: boolean | null;
    disabledFilter: boolean | null;
    searchQuery: string | null;
}

export interface IBusiness {
    name: string;
    businessId: string;
}

export interface IUpdateToggle {
    UserId: string | undefined;
    featureId: number;
    businessId: number | null;
    enableOrDisable: boolean;
}

export interface Ilog {
    logId: number,
    userId: string,
    userName: string,
    featureId: number,
    featureName: string,
    businessId: number | null,
    businessName: string | null,
    time: Date,
    action: number
}

export interface IPaginationLog {
    pageSize: number,
    currentPage: number,
    totalCount: number,
    totalPages: number,
    logs: Ilog[]
}


export interface ILoginReturn {
    token: string | null,
    errorMessage: string | null;

}
