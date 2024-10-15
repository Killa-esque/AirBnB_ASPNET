import React, { Suspense } from "react";
import { useRoutes } from "react-router-dom";
import { User } from "@/types";
import useAuth from "./useAuth";


const useRouteCustom = () => {

  const { user } = useAuth();

  const routes = useRoutes([

  ]);
}

export default useRouteCustom;
