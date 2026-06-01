export default {
    // get data from local
    async load() {
        try {
            const getReleases = await fetch(
                'https://server.pakeplus.com/public/latest/version'
            )
            const data = await getReleases.json()
            console.log('pakeplus.com data', JSON.stringify(data[0]))
            return data
        } catch (error) {
            console.error('pakeplus.com error', error)
            const getReleases = await fetch(
                'https://pakeplus.top/api/public/latest/version'
            )
            const data = await getReleases.json()
            console.log('pakeplus.top data', JSON.stringify(data[0]))
            return data
        }
    },
}
