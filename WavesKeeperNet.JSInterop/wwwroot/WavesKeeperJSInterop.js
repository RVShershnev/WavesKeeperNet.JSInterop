
export async function initialPromise() {
    try {
        WavesKeeper.initialPromise
            .then((keeperApi) => {              
                keeperApi.publicState().then(state => startApp(state));
            })
    }
    catch (err) {
        return err;
    }
}

export async function notification(title, message) {
    try {
        await WavesKeeper.notification({
            title: title,
            message: message
        });
    }
    catch (err) {
        return err;
    }
}

export async function encryptMessage(message, publicKey, appName) {
    try {
        let x = await WavesKeeper.encryptMessage(message, publicKey, appName);
        return { result: x };
    }
    catch (err) {
        return err;
    }
}

export async function decryptMessage(message, publicKey, appName) {
    try {
        let x = await WavesKeeper.decryptMessage(message, publicKey, appName);
        return { result: x };
    }
    catch (err) {
        return err;
    }
}



export async function publicState() {
    try {
        return await WavesKeeper.publicState()
    }
    catch (err) {
        return err;
    }
}

export async function auth(authData) {
    try {
        return await WavesKeeper.auth(authData)        
    }
    catch (err) {
        return err;
    }
}


export async function signTransaction(txData) {
    try    {
        let x = await WavesKeeper.signTransaction(txData);
        return { result: x };
    }
    catch (err) {
        return err;
    }
}

export async function signAndPublishTransaction(txData) {
    try { 
        let x = await WavesKeeper.signAndPublishTransaction(txData);
        return { result: x };
    }
    catch (err) {
        return err;
    }
}

export async function signTransactionPackage(tx, name) {
    try {      
        let x = await WavesKeeper.signTransactionPackage(tx, name);
        return { result: x };
    }
    catch (err) {
        return err;
    }
}

export async function signCustomData(version, binary) {
    try {
        let x = await WavesKeeper.signCustomData({
            version: version,
            binary: binary
        });       
        return { result: x };
    }
    catch (err) {
        return err;
    }
}

export async function verifyCustomData(version, binary, publicKey, signature) {
    try {
        let x = await WavesKeeper.verifyCustomData({
            "version": version,
            "binary": binary,
            "publicKey": publicKey,
            "signature": signature
        });
        return { "result": x };
    }
    catch (err) {
        return err;
    }
}

export async function resourceIsApproved() {
    try {
        return await WavesKeeper.resourceIsApproved();
    }
    catch (err) {
        return err;
    }
}

export async function resourceIsBlocked() {
    try {
        return await WavesKeeper.resourceIsBlocked();
    }
    catch (err) {
        return err;
    }
}


