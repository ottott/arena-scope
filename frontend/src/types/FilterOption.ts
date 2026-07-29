export interface FilterOption {
    type: "champion" | "item" | "augment";

    id: string | number;

    name: string;
    
    icon?: string;
}